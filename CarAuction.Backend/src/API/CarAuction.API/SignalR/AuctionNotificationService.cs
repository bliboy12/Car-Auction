using CarAuction.Api.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace CarAuction.Api.Hubs;

public class AuctionNotificationService : IAuctionNotificationService
{
    private readonly IHubContext<AuctionHub> _hubContext;

    public AuctionNotificationService(IHubContext<AuctionHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyBidPlacedAsync(BidPlacedNotification notification)
    {
        string groupName = AuctionHub.GetGroupName(notification.AuctionId);

        await _hubContext.Clients.Group(groupName).SendAsync("BidPlaced", notification);
    }

    public async Task NotifyAuctionActivatedAsync(Guid auctionId)
    {
        string groupName = AuctionHub.GetGroupName(auctionId);

        await _hubContext.Clients.Group(groupName).SendAsync("AuctionActivated", $"Auction: {auctionId} is now live!");
    }

    public async Task NotifyAuctionClosedAsync(AuctionClosedNotification notification)
    {
        string groupName = AuctionHub.GetGroupName(notification.AuctionId);

        await _hubContext.Clients.Group(groupName).SendAsync("AuctionClosed", notification);
    }
}