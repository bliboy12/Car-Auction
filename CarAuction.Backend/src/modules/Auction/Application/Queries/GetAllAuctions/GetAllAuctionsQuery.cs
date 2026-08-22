using MediatR;

public sealed record GetAllAuctionsQuery : IRequest<IEnumerable<AuctionDto>>;