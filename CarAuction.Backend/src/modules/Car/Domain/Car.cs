// Modules.Car.Domain
public class Car : Entity, IAggregateRoot
{
    public string Brand { get; private set; } = string.Empty;
    public string Model { get; private set; } = string.Empty;
    public string Trim { get; private set; } = string.Empty;
    public int Year { get; private set; }
    public int Kilometers { get; private set; }
    public bool HasDamage { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public string Color { get; private set; } = string.Empty;
    public string Fuel { get; private set; } = string.Empty;
    public Guid SellerId { get; private set; }

    private Car() { } // required by EF Core, kept private so nothing else uses it

    private Car(Guid id, string brand, string model, string trim, int year,
        int kilometers, bool hasDamage, string description, string color,
        string fuel, Guid sellerId) : base(id)
    {
        Brand = brand;
        Model = model;
        Trim = trim;
        Year = year;
        Kilometers = kilometers;
        HasDamage = hasDamage;
        Description = description;
        Color = color;
        Fuel = fuel;
        SellerId = sellerId;
    }

    public static Car CreateCar(string brand, string model, string trim, int year,
        int kilometers, bool hasDamage, string description, string color,
        string fuel, Guid sellerId)
    {
        if (string.IsNullOrWhiteSpace(brand))
            throw new ArgumentException("Brand is required.", nameof(brand));
        if (string.IsNullOrWhiteSpace(model))
            throw new ArgumentException("Model is required.", nameof(model));
        if (year < 1900 || year > DateTime.UtcNow.Year + 1)
            throw new ArgumentException("Year is invalid.", nameof(year));
        if (kilometers < 0)
            throw new ArgumentException("Kilometers cannot be negative.", nameof(kilometers));

        return new Car(Guid.NewGuid(), brand, model, trim, year, kilometers,
            hasDamage, description, color, fuel, sellerId);
    }

    public void UpdateDescription(string description) => Description = description;
}