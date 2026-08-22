public sealed record BidPlacedNotification
{
    public Guid AuctionId { get; }
    public Guid BidId { get; }
    public Guid BidderId { get; }
    public decimal NewPrice { get; }
    public DateTime EndTime { get; }
    public DateTime BidTime { get; }

    public BidPlacedNotification(Guid auctionId, Guid bidId, Guid bidderId, decimal newPrice, DateTime endTime, DateTime bidTime)
    {
        AuctionId = auctionId;
        BidId = bidId;
        BidderId = bidderId;
        NewPrice = newPrice;
        EndTime = endTime;
        BidTime = bidTime;
    }
}