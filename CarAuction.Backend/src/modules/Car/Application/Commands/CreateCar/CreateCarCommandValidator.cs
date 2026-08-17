using FluentValidation;

public class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
{
    public CreateCarCommandValidator()
    {
        RuleFor(x => x.Brand).NotEmpty().WithMessage("Brand needs to be provided");
        RuleFor(x => x.Model).NotEmpty().WithMessage("Model needs to be provided");
        RuleFor(x => x.Trim).NotEmpty().WithMessage("Trim needs to be provided");
        RuleFor(x => x.Color).NotEmpty().WithMessage("Color needs to be provided");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description needs to be provided");
        RuleFor(x => x.Fuel).NotEmpty().WithMessage("Fuel needs to be provided");
        RuleFor(x => x.SellerId).NotEmpty().WithMessage("Seller needs to be provided");
        RuleFor(x => x.Year).LessThanOrEqualTo(DateTime.UtcNow.Year).WithMessage("Build year can not be larger then current year").GreaterThanOrEqualTo(1900).WithMessage("Build year can only start from the year 1900");
        RuleFor(x => x.Kilometers).GreaterThanOrEqualTo(0).WithMessage("Kilometers can not be smaller then zero");
    }
}