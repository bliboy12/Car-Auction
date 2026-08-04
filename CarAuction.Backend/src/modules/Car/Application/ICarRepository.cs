public interface ICarRepository
{
    Task AddAsync(Car car);
    Task<Car?> GetByIdAsync(Guid id);
}