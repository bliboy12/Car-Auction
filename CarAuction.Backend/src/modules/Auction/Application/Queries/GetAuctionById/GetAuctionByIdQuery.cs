using MediatR;

public sealed record GetAuctionByIdQuery(Guid Id) : IRequest<AuctionDto>;