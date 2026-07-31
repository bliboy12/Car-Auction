public class Auction : Entity, IAggregateRoot
{
    public Guid AuctionId { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public Guid CarId { get; private set; }
    public Guid SellerId { get; private set; }
    public AuctionStatus Status { get; private set; }
    public decimal CurrentPrice { get; private set; }

    private Auction() { } // For EF Core

    private Auction(Guid id, DateTime startTime, DateTime endTime, Guid carId, Guid sellerId, AuctionStatus status, decimal currentPrice)
    {
        StartTime = startTime;
        EndTime = endTime;
        CarId = carId;
        SellerId = sellerId;
        Status = status;
        CurrentPrice = currentPrice;
    }

    // We don't let the user decide on the status of the Auction
    // An Auction must be created with the status scheduled and user is only able to change Sold/Unsold (or I'm still thinking through this)
    public static Auction CreateAuction(DateTime startTime, DateTime endTime, Guid carId, Guid sellerId, decimal currentPrice)
    {
        if (startTime >= endTime)
            throw new ArgumentException("Start Time can not be later then End Time");
        if (startTime < DateTime.UtcNow)
            throw new ArgumentException("Start Time can not be in the past");
        if (currentPrice <= 0)
            throw new ArgumentException("Current price can not be lower then or equal to zero");


        return new Auction(Guid.NewGuid(), startTime, endTime, carId, sellerId, AuctionStatus.Scheduled, currentPrice);
    }

    public void UpdateStartEndTimes(DateTime startTime, DateTime endTime)
    {
        if (startTime >= endTime)
            throw new ArgumentException("Start Time can not be later then End Time");
        if (startTime < DateTime.UtcNow)
            throw new ArgumentException("Start Time can not be in the past");

        StartTime = startTime;
        EndTime = endTime;
    }
    public void UpdateStatus(AuctionStatus status)
    {
        if (Status == AuctionStatus.Sold || Status == AuctionStatus.Unsold)
            throw new ArgumentException("Auction has finished, can no longer make changes to status!");
        if (Status == AuctionStatus.Active && (DateTime.UtcNow >= StartTime && DateTime.UtcNow <= EndTime))
            throw new ArgumentException("Auction is on going, can make no changes until its finished");
        if (DateTime.UtcNow > StartTime && status == (Auction.Active || AuctionStatus.Sold || AuctionStatus.Unsold))
            throw new ArgumentException("Auction Status can not be decided before the start time");

        if (DateTime.UtcNow > EndTime && (Status && status) != (AuctionStatus.Sold || AuctionStatus.unsold))
            throw new ArgumentException("Auction status must be decided upon, either Sold or Unsold");

        Status = status;
    }

    public void UpdateCurrentPrice(decimal currentPrice)
    {
        if (currentPrice < CurrentPrice)
            throw new ArgumentException("Price must be larger then the current price");

        if (Status == (AuctionStatus.Unsold || AuctionStatus.Sold))
            throw new ArgumentException("Price can no longer be changed, auction came to an end");

        CurrentPrice = currentPrice;
    }
}