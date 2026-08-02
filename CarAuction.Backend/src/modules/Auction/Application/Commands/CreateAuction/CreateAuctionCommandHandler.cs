public class CreateAuctionCommandHandler
{
    private IAuctionRepository _repo;
    private IUnitOfWork _unitOfWork;

    public CreateAuctionCommandHandler(IAuctionRepository repo, IUnitOfWork unitOfWork)
    {
        _repo = repo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateAuctionCommand request)
    {
        Auction auction = Auction.CreateAuction(request.StartTime, request.EndTime, request.CarId, request.SellerId, request.StartingPrice);

        await _repo.AddAsync(auction);
        await _unitOfWork.SaveChangesAsync();

        return auction.Id;
    }
}