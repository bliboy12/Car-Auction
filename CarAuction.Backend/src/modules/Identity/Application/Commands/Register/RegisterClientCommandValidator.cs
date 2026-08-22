using FluentValidation;

public sealed class RegisterClientCommandValidator : AbstractValidator<RegisterClientCommand>
{
    public RegisterClientCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress().WithMessage("Must be a valid Email Adress");
        RuleFor(p => p.Password).NotEmpty().WithMessage("Your password cannot be empty")
            .MinimumLength(8).WithMessage("Your password length must be at least 8.")
            .MaximumLength(50).WithMessage("Your password length must not exceed 16.")
            .Matches("[A-Z]").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Your password must contain at least one number.")
            .Matches(@"[\!\?\*\.]").WithMessage("Your password must contain at least one (!? *.).");
        RuleFor(x => x.BirthDate).Must(birthDate => birthDate <= DateOnly.FromDateTime(DateTime.Now).AddYears(-18)).WithMessage("Person must be at least 18 years old.");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name must be provided").MaximumLength(100);
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name must be provided").MaximumLength(100);
        RuleFor(x => x.Address).NotNull().WithMessage("Address needs to be provided");
    }
}