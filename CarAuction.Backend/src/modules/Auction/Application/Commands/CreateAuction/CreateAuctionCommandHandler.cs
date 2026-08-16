using MediatR;

public class CreateAuctionCommandHandler : IRequestHandler<CreateAuctionCommand, Guid>
{
    private readonly IAuctionRepository _auctionRepo;
    private readonly ICarRepository _carRepo;
    private IUnitOfWork _unitOfWork;

    public CreateAuctionCommandHandler(IAuctionRepository repo, ICarRepository carRepo, IUnitOfWork unitOfWork)
    {
        _auctionRepo = repo;
        _carRepo = carRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateAuctionCommand request, CancellationToken cancellationToken)
    {
        Car? car = await _carRepo.GetByIdAsync(request.CarId);

        if (car is null)
            throw new ArgumentException($"Car with ID: {request.CarId} Not Found");
        if (car.SellerId != request.SellerId)
            throw new ArgumentException("Can only list cars you own");

        Auction auction = Auction.CreateAuction(request.StartTime, request.EndTime, request.CarId, request.SellerId, request.StartingPrice);

        await _auctionRepo.AddAsync(auction);
        await _unitOfWork.SaveChangesAsync();

        return auction.Id;
    }
}