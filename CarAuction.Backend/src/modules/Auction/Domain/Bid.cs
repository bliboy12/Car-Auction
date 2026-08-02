public class Bid : Entity
{
    public Guid AuctionId { get; private set; }
    public Guid BidderId { get; private set; }
    public decimal Price { get; private set; }
    public DateTime Time { get; private set; }

    private Bid() { }

    private Bid(Guid id, Guid bidderId, Guid auctionId, decimal price, DateTime time) : base(id)
    {
        BidderId = bidderId;
        AuctionId = auctionId;
        Price = price;
        Time = time;
    }

    public static Bid CreateNewBid(Guid bidderId, Guid auctionId, decimal price)
    {
        return new Bid(Guid.NewGuid(), bidderId, auctionId, price, DateTime.UtcNow);
    }
}