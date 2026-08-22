using MediatR;

public sealed record CreateCarCommand(string Brand, string Model, string Trim, int Year, int Kilometers, bool HasDamage, string Description, string Color, string Fuel, Guid SellerId) : IRequest<Guid>;