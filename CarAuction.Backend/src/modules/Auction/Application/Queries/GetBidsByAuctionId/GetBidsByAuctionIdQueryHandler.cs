using MediatR;

public class GetBidsByAuctionIdQueryHandler : IRequestHandler<GetBidsByAuctionIdQuery, IEnumerable<BidDto>>
{
    private IAuctionRepository _auctionRepo;
    private IBidRepository _bidRepo;

    public GetBidsByAuctionIdQueryHandler(IAuctionRepository auctionRepo, IBidRepository bidRepo)
    {
        _auctionRepo = auctionRepo;
        _bidRepo = bidRepo;
    }

    public async Task<IEnumerable<BidDto>> Handle(GetBidsByAuctionIdQuery request, CancellationToken cancellationToken)
    {
        // instead of loading the full object just to check if it exists, created instead a separate method for that check
        // Auction? auction = await _auctionRepo.GetByIdAsync(request.AuctionId);

        bool auctionExists = await _auctionRepo.ExistsAsync(request.AuctionId);

        if (!auctionExists)
            throw new ArgumentException("Auction not found");

        IEnumerable<Bid> bids = await _bidRepo.GetByAuctionIdAsync(request.AuctionId);
        IEnumerable<BidDto> bidDtos = bids.Select(bid => new BidDto(bid.Id, bid.AuctionId, bid.BidderId, bid.Price, bid.Time));

        return bidDtos;
    }
}