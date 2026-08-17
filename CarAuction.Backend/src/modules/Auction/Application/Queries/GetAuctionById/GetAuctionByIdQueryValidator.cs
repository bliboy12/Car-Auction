using FluentValidation;

// This class isn't a must but for consistency and learning purpose I have created it aswel 
public class GetAuctionByIdQueryValidator : AbstractValidator<GetAuctionByIdQuery>
{
    public GetAuctionByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Auction id must be specified");
    }
}