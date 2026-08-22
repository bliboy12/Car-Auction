using MediatR;

public sealed record GetBidsByAuctionIdQuery(Guid AuctionId) : IRequest<IEnumerable<BidDto>>;