using MediatR;

public class PlaceBidCommandHandler : IRequestHandler<PlaceBidCommand, Guid>
{
    private IAuctionRepository _auctionRepo;
    private IBidRepository _bidRepo;
    private IUnitOfWork _unitOfWork;

    public PlaceBidCommandHandler(IAuctionRepository auctionRepo, IBidRepository bidRepo, IUnitOfWork unitOfWork)
    {
        _auctionRepo = auctionRepo;
        _bidRepo = bidRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(PlaceBidCommand request, CancellationToken cancellationToken)
    {
        // First fetch the auction and see if it exists
        Auction? auction = await _auctionRepo.GetByIdAsync(request.AuctionId);
        // Throw exception if it doesn't
        if (auction is null)
            throw new ArgumentException("Auction not found");
        // The auction holds the correct method that performs the checks and create the bid for us
        Bid newBid = auction.PlaceBid(request.BidderId, request.Price);
        // We add it in the DB (EF Core)
        await _bidRepo.AddAsync(newBid);
        // Save the changes being tracked by EF Core
        await _unitOfWork.SaveChangesAsync();
        // Return the minimum to indicate success of this operation (not full object, that is for Query)
        return newBid.Id;
    }
}