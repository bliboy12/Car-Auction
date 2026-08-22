using FluentValidation;

public sealed class PlaceBidCommandValidator : AbstractValidator<PlaceBidCommand>
{
    public PlaceBidCommandValidator()
    {
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price can not be lower or equal to zero!");
        RuleFor(x => x.AuctionId).NotEmpty().WithMessage("Auction must be specified");
        RuleFor(x => x.BidderId).NotEmpty().WithMessage("Bidder must be specified");
    }
}