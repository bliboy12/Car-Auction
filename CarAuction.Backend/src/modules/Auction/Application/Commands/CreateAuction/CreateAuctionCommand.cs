using MediatR;

public class CreateAuctionCommand : IRequest<Guid>
{
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
    public Guid CarId { get; }
    public Guid SellerId { get; }
    public decimal StartingPrice { get; }

    public CreateAuctionCommand(DateTime startTime, DateTime endTime, Guid carId, Guid sellerId, decimal startingPrice)
    {
        StartTime = startTime;
        EndTime = endTime;
        CarId = carId;
        SellerId = sellerId;
        StartingPrice = startingPrice;
    }

}