public class Auction : Entity, IAggregateRoot
{
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public Guid CarId { get; private set; }
    public Guid SellerId { get; private set; }
    public Guid? WinningBid { get; private set; }
    public AuctionStatus Status { get; private set; }
    public decimal CurrentPrice { get; private set; }
    public decimal StartingPrice { get; private set; }

    // We left this out here because loading the full bid history alongside Auction would be a performance problem in a hyped auction (most of the time, the last 10 seconds before close)
    // public IEnumerable<Bid> Bids { get; private set; } = new List<Bid>();

    private Auction() { } // For EF Core

    private Auction(Guid id, DateTime startTime, DateTime endTime, Guid carId, Guid sellerId, AuctionStatus status, decimal startingPrice, Guid? winningBid) : base(id)
    {
        StartTime = startTime;
        EndTime = endTime;
        CarId = carId;
        SellerId = sellerId;
        Status = status;
        StartingPrice = CurrentPrice = startingPrice; // CurrentPrice and StartingPrice should have the same value to start
        WinningBid = winningBid;
    }

    // We don't let the user decide on the status of the Auction
    // An Auction must be created with the status scheduled and user is only able to change Sold/Unsold (or I'm still thinking through this)
    public static Auction CreateAuction(DateTime startTime, DateTime endTime, Guid carId, Guid sellerId, decimal startingPrice)
    {
        if (startTime >= endTime)
            throw new ArgumentException("Start Time can not be later then End Time");
        if (startTime < DateTime.UtcNow)
            throw new ArgumentException("Start Time can not be in the past");
        if (startingPrice <= 0)
            throw new ArgumentException("Starting price can not be lower then or equal to zero");


        return new Auction(Guid.NewGuid(), startTime, endTime, carId, sellerId, AuctionStatus.Scheduled, startingPrice, null);
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
        int minimumBid = 1;

        if (Status != AuctionStatus.Active)
            throw new ArgumentException("Can't place a bid with an InActive Auction");
        if (amount <= CurrentPrice)
            throw new ArgumentException("Bid can't be smaller then current price");
        if (amount - CurrentPrice < minimumBid)
            throw new ArgumentException($"Bid must be at least €{minimumBid}");
        if (bidderId == Guid.Empty)
            throw new ArgumentException("Bidder must be specified");

        Bid newBid = Bid.CreateNewBid(bidderId, Id, amount);
        CurrentPrice = amount;

        WinningBid = newBid.Id; // tracking who the current winning bidder is

        return newBid;
    }

}