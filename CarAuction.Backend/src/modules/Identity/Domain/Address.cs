public class Address
{
    public string StreetName { get; private set; } = string.Empty;
    public string HouseNumber { get; private set; } = string.Empty;
    public string PostalCode { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;

    public Address(string streetName, string houseNumber, string postalCode, string city, string country)
    {
        if (String.IsNullOrWhiteSpace(streetName))
            throw new ArgumentException("Street name is not allowed to be empty");
        if (String.IsNullOrWhiteSpace(houseNumber))
            throw new ArgumentException("House number is not allowed to be empty");
        if (String.IsNullOrWhiteSpace(postalCode))
            throw new ArgumentException("Postalcode is not allowed to be empty");
        if (String.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City is not allowed to be empty");
        if (String.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country is not allowed to be empty");
        StreetName = streetName;
        HouseNumber = houseNumber;
        PostalCode = postalCode;
        City = city;
        Country = country;
    }

}