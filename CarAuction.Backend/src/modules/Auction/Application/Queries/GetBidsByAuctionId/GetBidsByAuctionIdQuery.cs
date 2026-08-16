using MediatR;

public class GetBidsByAuctionIdQuery : IRequest<IEnumerable<BidDto>>
{
    public Guid AuctionId { get; private set; }

    public GetBidsByAuctionIdQuery(Guid auctionId)
    {
        AuctionId = auctionId;
    }
}