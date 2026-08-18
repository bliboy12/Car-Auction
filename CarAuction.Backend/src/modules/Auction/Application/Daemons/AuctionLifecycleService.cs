using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class AuctionLifecycleService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<AuctionLifecycleService> _logger;

    public AuctionLifecycleService(IServiceScopeFactory scopeFactory, ILogger<AuctionLifecycleService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var auctionRepo = scope.ServiceProvider.GetRequiredService<IAuctionRepository>();
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
                }

                if (toActivate.Any() || toClose.Any())
                    await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error running auction lifecycle sweep.");
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}