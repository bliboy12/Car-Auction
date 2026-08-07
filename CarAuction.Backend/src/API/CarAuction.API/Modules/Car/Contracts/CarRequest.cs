public class CarRequest
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

    public CarRequest(string brand, string model, string trim, int year, int kilometers, bool hasDamage, string description, string color, string fuel)
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
    }
}