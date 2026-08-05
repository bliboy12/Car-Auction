using CarAuction.Persistence;
using Microsoft.EntityFrameworkCore;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context) => _context = context;
    public async Task AddAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
    }

    public async Task<Transaction?> GetByIdAsync(Guid id)
    {
        return await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id);
    }
    public async Task<Transaction?> GetByWinningBidIdAsync(Guid winningBid)
    {
        return await _context.Transactions.FirstOrDefaultAsync(t => t.WinningBidId == winningBid);
    }
}