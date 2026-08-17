using FluentValidation;

// This class isn't a must but for consistency and learning purpose I have created it aswel 
public class GetCarByIdQueryValidator : AbstractValidator<GetCarByIdQuery>
{
    public GetCarByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Car Id must be provided");
    }
}