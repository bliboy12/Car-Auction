using FluentValidation;
public sealed class CreateAuctionCommandValidator : AbstractValidator<CreateAuctionCommand>
{
    public CreateAuctionCommandValidator()
    {
        RuleFor(x => x.StartTime).GreaterThan(DateTime.UtcNow).WithMessage("Start time must be in the future");
        RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime).WithMessage("End time must be after start time");
        RuleFor(x => x.StartingPrice).GreaterThan(0).WithMessage("Starting price must be greater then zero");
        RuleFor(x => x.CarId).NotEmpty().WithMessage("A car must be specified");
        RuleFor(x => x.SellerId).NotEmpty().WithMessage("Seller could not be identified");
    }
}