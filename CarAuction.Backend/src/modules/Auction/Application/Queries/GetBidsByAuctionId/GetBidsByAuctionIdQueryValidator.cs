using FluentValidation;

// This class isn't a must but for consistency and learning purpose I have created it aswel 
public class GetBidsByAuctionByIdQueryValidator : AbstractValidator<GetBidsByAuctionIdQuery>
{
    public GetBidsByAuctionByIdQueryValidator()
    {
        RuleFor(x => x.AuctionId).NotEmpty().WithMessage("An Auction Id must be specified");
    }
}