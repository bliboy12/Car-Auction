public class RegisterClientCommand
{
    public string Email { get; } = string.Empty;
    public string Password { get; } = string.Empty;
    public string FirstName { get; } = string.Empty;
    public string LastName { get; } = string.Empty;
    public DateTime BirthDate { get; }
    public Address Address { get; }

    public RegisterClientCommand(string email, string password, string firstName, string lastName, DateTime birthDate, Address address)
    {
        Email = email;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Address = address;
    }
}