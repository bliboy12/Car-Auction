using MediatR;

public class RegisterClientCommand : IRequest<LoginResult>
{
    public string Email { get; } = string.Empty;
    public string Password { get; } = string.Empty;
    public string FirstName { get; } = string.Empty;
    public string LastName { get; } = string.Empty;
    public DateOnly BirthDate { get; }
    public Address Address { get; }

    public RegisterClientCommand(string email, string password, string firstName, string lastName, DateOnly birthDate, Address address)
    {
        Email = email;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Address = address;
    }
}