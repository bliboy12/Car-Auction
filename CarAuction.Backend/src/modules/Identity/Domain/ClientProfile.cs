public class ClientProfile : Entity, IAggregateRoot
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public DateTime BirthDate { get; private set; }
    public Address Address { get; private set; }

    private ClientProfile() { } // For EF Core

    private ClientProfile(Guid id, string firstName, string lastName, DateTime birthDate, Address address) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Address = address;
    }

    public static ClientProfile CreateClientProfile(Guid id, string firstName, string lastName, DateTime birthDate, Address address)
    {
        int minimumAge = 18;

        if (String.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("Firstname can not be empty");
        if (String.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Lastname can not be empty");

        int age = DateTime.UtcNow.Year - birthDate.Year;
        if (birthDate.Date > DateTime.UtcNow.AddYears(-age))
            age--;

        if (age < minimumAge)
            throw new ArgumentException($"User must be at least {minimumAge} years of age");

        return new ClientProfile(id, firstName, lastName, birthDate, address);
    }
}