using FluentValidation;

public sealed class GetClientProfileByIdQueryValidator : AbstractValidator<GetClientProfileByIdQuery>
{
    public GetClientProfileByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Client Id can not be empty");
    }
}