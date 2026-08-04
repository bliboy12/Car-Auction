public class CreateClientProfileCommand
{
    public Guid Id { get; }
    public string FirstName { get; } = string.Empty;
    public string LastName { get; } = string.Empty;
    public DateTime BirthDate { get; }
    public Address Address { get; }
    public string Email { get; } = string.Empty;

    public CreateClientProfileCommand(Guid id, string firstName, string lastName, DateTime birthDate, Address address, string email)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Address = address;
        Email = email;
    }
}