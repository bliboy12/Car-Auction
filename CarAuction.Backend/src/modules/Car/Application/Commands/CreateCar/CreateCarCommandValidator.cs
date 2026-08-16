using FluentValidation;

public class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
{
    public CreateCarCommandValidator()
    {
        RuleFor(x => x.Brand).NotEmpty().WithMessage("Brand needs to be provided");
        RuleFor(x => x.Brand).NotEmpty().WithMessage("Brand needs to be provided");
        RuleFor(x => x.Brand).NotEmpty().WithMessage("Brand needs to be provided");
        RuleFor(x => x.Brand).NotEmpty().WithMessage("Brand needs to be provided");
        RuleFor(x => x.Year).GreaterThanOrEqualTo(1900).WithMessage("Build year can only start from the year 1900");
        RuleFor(x => x.Kilometers).GreaterThanOrEqualTo(0).WithMessage("Brand needs to be provided"); // FInish Validator for car and identity
    }
}