public interface IAuctionRepository
{
    Task<Auction?> GetByIdAsync(Guid auctionId);
    Task AddAsync(Auction auction);
    Task<bool> ExistsAsync(Guid auctionId);
}