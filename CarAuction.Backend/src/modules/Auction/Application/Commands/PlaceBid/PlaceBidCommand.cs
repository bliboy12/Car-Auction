public class PlaceBidCommand
{
    public Guid AuctionId { get; private set; }
    public Guid BidderId { get; private set; }
    public decimal Price { get; private set; }

    public PlaceBidCommand(Guid auctionId, Guid bidderId, decimal price)
    {
        AuctionId = auctionId;
        BidderId = bidderId;
        Price = price;
    }
}