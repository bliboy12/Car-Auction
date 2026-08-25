using MediatR;

public sealed class GetAllAuctionsQueryHandler : IRequestHandler<GetAllAuctionsQuery, IEnumerable<AuctionDto>>
{
    private readonly IAuctionRepository _repo;
    public GetAllAuctionsQueryHandler(IAuctionRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<AuctionDto>> Handle(GetAllAuctionsQuery request, CancellationToken cancellationToken)
    {
        var auctions = await _repo.GetAllAuctionsAsync();
        return auctions.Select(a => new AuctionDto(a.Id, a.StartTime, a.EndTime, a.CarId, a.SellerId, a.Status, a.CurrentPrice, a.StartingPrice, a.WinningBid));
    }
}