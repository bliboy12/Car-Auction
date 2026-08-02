public class AuctionDto
{
    public Guid Id { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
    public Guid CarId { get; }
    public Guid SellerId { get; }
    public AuctionStatus Status { get; }
    public decimal CurrentPrice { get; }

    public AuctionDto(Guid id, DateTime startTime, DateTime endTime, Guid carId, Guid sellerId, AuctionStatus status, decimal currentPrice)
    {
        Id = id;
        StartTime = startTime;
        EndTime = endTime;
        CarId = carId;
        SellerId = sellerId;
        Status = status;
        CurrentPrice = currentPrice;
    }
}