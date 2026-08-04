public class GetCarByIdQueryHandler
{
    private ICarRepository _carRepo;

    public GetCarByIdQueryHandler(ICarRepository carRepo)
    {
        _carRepo = carRepo;
    }

    public async Task<CarDto> Handle(GetCarByIdQuery request)
    {
        Car? car = await _carRepo.GetByIdAsync(request.Id);

        if (car is null)
            throw new ArgumentException("Car not found");

        CarDto carDto = new CarDto(car.Id, car.Brand, car.Model, car.Trim, car.Year, car.Kilometers, car.HasDamage, car.Description, car.Color, car.Fuel, car.SellerId);

        return carDto;
    }
}