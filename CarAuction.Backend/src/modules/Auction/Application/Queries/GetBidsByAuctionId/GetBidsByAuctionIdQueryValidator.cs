using FluentValidation;

public class GetBidsByAuctionByIdQueryValidator : AbstractValidator<GetBidsByAuctionIdQuery>
{
    public GetBidsByAuctionByIdQueryValidator()
    {
        RuleFor(x => x.AuctionId).NotEmpty().WithMessage("An Auction Id must be specified");
    }
}