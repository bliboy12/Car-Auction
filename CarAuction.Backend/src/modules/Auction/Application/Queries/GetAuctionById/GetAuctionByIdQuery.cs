public class GetAuctionByIdQuery
{
    public Guid Id { get; private set; }

    public GetAuctionByIdQuery(Guid id)
    {
        Id = id;
    }
}