public class AuctionClosedNotification
{
    public Guid AuctionId { get; }
    public decimal FinalPrice { get; }
    public bool IsSold { get; }

    public AuctionClosedNotification(Guid auctionId, decimal finalPrice, bool isSold)
    {
        AuctionId = auctionId;
        FinalPrice = finalPrice;
        IsSold = isSold;
    }
}