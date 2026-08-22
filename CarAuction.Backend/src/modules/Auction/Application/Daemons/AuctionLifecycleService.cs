using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public sealed class AuctionLifecycleService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<AuctionLifecycleService> _logger;
    private readonly IAuctionNotificationService _notificationService;

    public AuctionLifecycleService(IServiceScopeFactory scopeFactory, ILogger<AuctionLifecycleService> logger, IAuctionNotificationService notificationService)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _notificationService = notificationService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var auctionRepo = scope.ServiceProvider.GetRequiredService<IAuctionRepository>();
                var transactionRepo = scope.ServiceProvider.GetRequiredService<ITransactionRepository>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var toActivate = await auctionRepo.GetAuctionsToActivateAsync();
                foreach (var auction in toActivate)
                {
                    auction.Activate();
                    _logger.LogInformation("Activated auction {AuctionId}", auction.Id);
                }

                var toClose = await auctionRepo.GetAuctionsToCloseAsync();
                foreach (var auction in toClose)
                {
                    auction.Close();
                    _logger.LogInformation("Closed auction {AuctionId} as {Status}", auction.Id, auction.Status);

                    if (auction.Status == AuctionStatus.Sold)
                    {
                        var transaction = Transaction.CreateNewTransaction(auction.WinningBid!.Value);
                        await transactionRepo.AddAsync(transaction);
                    }
                }

                if (toActivate.Any() || toClose.Any())
                    await unitOfWork.SaveChangesAsync();

                // We only send notification after changes have saved successfully and not before
                foreach (var auction in toActivate)
                    await _notificationService.NotifyAuctionActivatedAsync(auction.Id);

                foreach (var auction in toClose)
                {
                    var notification = new AuctionClosedNotification(auction.Id, auction.CurrentPrice, auction.Status == AuctionStatus.Sold);
                    await _notificationService.NotifyAuctionClosedAsync(notification);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error running auction lifecycle sweep.");
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}