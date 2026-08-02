public class BidDto
{
    public Guid Id { get; }
    public Guid AuctionId { get; }
    public Guid BidderId { get; }
    public decimal Price { get; }
    public DateTime Time { get; }

    public BidDto(Guid id, Guid auctionId, Guid bidderId, decimal price, DateTime time)
    {
        Id = id;
        AuctionId = auctionId;
        BidderId = bidderId;
        Price = price;
        Time = time;
    }
}