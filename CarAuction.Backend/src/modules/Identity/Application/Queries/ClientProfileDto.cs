public class ClientProfileDto
{
    public Guid Id { get; }
    public string FirstName { get; } = string.Empty;
    public string LastName { get; } = string.Empty;
    public DateOnly BirthDate { get; }
    public Address Address { get; }
    public string Email { get; } = string.Empty;

    public ClientProfileDto(Guid id, string firstName, string lastName, DateOnly birthDate, Address address, string email)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Address = address;
        Email = email;
    }
}