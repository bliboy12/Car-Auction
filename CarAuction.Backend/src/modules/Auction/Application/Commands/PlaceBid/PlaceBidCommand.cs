using MediatR;

public sealed record PlaceBidCommand(Guid AuctionId, Guid BidderId, decimal Price) : IRequest<Guid>;