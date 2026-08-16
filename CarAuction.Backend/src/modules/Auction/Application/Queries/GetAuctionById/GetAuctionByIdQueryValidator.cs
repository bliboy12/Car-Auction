using FluentValidation;

public class GetAuctionByIdQueryValidator : AbstractValidator<GetAuctionByIdQuery>
{
    public GetAuctionByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Auction id must be specified");
    }
}