using MediatR;

public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, Guid>
{
    private ICarRepository _repo;
    private IUnitOfWork _unitOfWork;

    public CreateCarCommandHandler(ICarRepository repo, IUnitOfWork unitOfWork)
    {
        _repo = repo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        Car car = Car.CreateCar(request.Brand, request.Model, request.Trim, request.Year, request.Kilometers, request.HasDamage, request.Description, request.Color, request.Fuel, request.SellerId);

        await _repo.AddAsync(car);
        await _unitOfWork.SaveChangesAsync();

        return car.Id;
    }
}