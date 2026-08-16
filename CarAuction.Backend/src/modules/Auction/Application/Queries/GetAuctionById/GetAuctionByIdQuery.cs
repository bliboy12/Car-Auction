using MediatR;

public class GetAuctionByIdQuery : IRequest<AuctionDto>
{
    public Guid Id { get; private set; }

    public GetAuctionByIdQuery(Guid id)
    {
        Id = id;
    }
}