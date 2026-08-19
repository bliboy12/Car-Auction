public interface IAuctionNotificationService
{
    Task NotifyBidPlacedAsync(BidPlacedNotification notification);
    Task NotifyAuctionActivatedAsync(Guid auctionId);
    Task NotifyAuctionClosedAsync(AuctionClosedNotification notification);
}