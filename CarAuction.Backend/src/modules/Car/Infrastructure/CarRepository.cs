using CarAuction.Persistence;
using Microsoft.EntityFrameworkCore;

public sealed class CarRepository : ICarRepository
{
    private readonly AppDbContext _context;

    public CarRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Car car)
    {
        await _context.Cars.AddAsync(car);
    }

    public async Task<Car?> GetByIdAsync(Guid id)
    {
        return await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
    }
}