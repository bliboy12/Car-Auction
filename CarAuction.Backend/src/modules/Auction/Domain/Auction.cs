public class Auction : Entity, IAggregateRoot
{
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public Guid CarId { get; private set; }
    public Guid SellerId { get; private set; }
    public AuctionStatus Status { get; private set; }
    public decimal CurrentPrice { get; private set; }
    public decimal StartingPrice { get; private set; }

    // We left this out here because loading the full bid history alongside Auction would be a performance problem in a hyped auction (most of the time, the last 10 seconds before close)
    // public IEnumerable<Bid> Bids { get; private set; } = new List<Bid>();

    private Auction() { } // For EF Core

    private Auction(Guid id, DateTime startTime, DateTime endTime, Guid carId, Guid sellerId, AuctionStatus status, decimal startingPrice) : base(id)
    {
        StartTime = startTime;
        EndTime = endTime;
        CarId = carId;
        SellerId = sellerId;
        Status = status;
        StartingPrice = CurrentPrice = startingPrice; // CurrentPrice and StartingPrice should have the same value to start
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

    public void Activate()
    {
        if (Status != AuctionStatus.Scheduled)
            throw new ArgumentException("Only a schedulded auction can be activated");
        if (DateTime.UtcNow < StartTime)
            throw new ArgumentException("Auction cannot be activated before start time");
        Status = AuctionStatus.Active;
    }

    public void Close()
    {
        if (Status != AuctionStatus.Active)
            throw new ArgumentException("Only a active auction can be closed");
        if (DateTime.UtcNow < EndTime)
            throw new ArgumentException("Auction cannot be closed before end time");

        Status = StartingPrice != CurrentPrice ? AuctionStatus.Sold : AuctionStatus.Unsold;
    }
    public Bid PlaceBid(Guid bidderId, decimal amount)
    {
        if (Status != AuctionStatus.Active)
            throw new ArgumentException("Can't place a bid with an InActive Auction");
        if (amount <= CurrentPrice)
            throw new ArgumentException("Bid can't be smaller then current price");

        Bid newBid = Bid.CreateNewBid(bidderId, Id, amount);
        CurrentPrice = amount;

        return newBid;
    }
}