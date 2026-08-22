using FluentValidation;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress().WithMessage("Must be a valid email adress");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password must not be empty");
    }
}