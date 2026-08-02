public interface IAuctionRepository
{
    Task<Auction?> GetByIdAsync(Guid auctionId);
    Task AddAsync(Auction auction);
}