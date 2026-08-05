public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
    Task<Transaction?> GetByIdAsync(Guid id);
    Task<Transaction?> GetByWinningBidIdAsync(Guid winningBid);
}