using MediatR;

public sealed record CreateAuctionCommand(DateTime StartTime, DateTime EndTime, Guid CarId, Guid SellerId, decimal StartingPrice) : IRequest<Guid>;
