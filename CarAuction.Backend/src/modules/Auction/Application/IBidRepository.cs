public interface IBidRepository
{
    Task AddAsync(Bid bid);
    Task<IReadOnlyList<Bid>> GetByAuctionIdAsync(Guid auctionId);
}