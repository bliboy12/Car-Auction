public class GetBidsByAuctionIdQuery
{
    public Guid AuctionId { get; private set; }

    public GetBidsByAuctionIdQuery(Guid auctionId)
    {
        AuctionId = auctionId;
    }
}