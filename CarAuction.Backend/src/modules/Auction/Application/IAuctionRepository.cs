public interface IAuctionRepository
{
    Task<Auction?> GetByIdAsync(Guid auctionId);
    Task AddAsync(Auction auction);
    Task<bool> ExistsAsync(Guid auctionId);
    Task<IReadOnlyList<Auction>> GetAuctionsToActivateAsync();
    Task<IReadOnlyList<Auction>> GetAuctionsToCloseAsync();
}