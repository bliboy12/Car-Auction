using MediatR;

public class GetAuctionByIdQueryHandler : IRequestHandler<GetAuctionByIdQuery, AuctionDto>
{
    private IAuctionRepository _repo;

    public GetAuctionByIdQueryHandler(IAuctionRepository repo)
    {
        _repo = repo;
    }

    public async Task<AuctionDto> Handle(GetAuctionByIdQuery request, CancellationToken cancellationToken)
    {
        Auction? auction = await _repo.GetByIdAsync(request.Id);

        if (auction is null)
            throw new ArgumentException("Auction not found");

        return new AuctionDto(auction.Id, auction.StartTime, auction.EndTime, auction.CarId, auction.SellerId, auction.Status, auction.CurrentPrice);
    }
}