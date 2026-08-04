public class CarDto
{
    public Guid Id { get; }
    public string Brand { get; } = string.Empty;
    public string Model { get; } = string.Empty;
    public string Trim { get; } = string.Empty;
    public int Year { get; }
    public int Kilometers { get; }
    public bool HasDamage { get; }
    public string Description { get; } = string.Empty;
    public string Color { get; } = string.Empty;
    public string Fuel { get; } = string.Empty;
    public Guid SellerId { get; }

    public CarDto(Guid id, string brand, string model, string trim, int year, int kilometers, bool hasDamage, string description, string color, string fuel, Guid sellerId)
    {
        Id = id;
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
}