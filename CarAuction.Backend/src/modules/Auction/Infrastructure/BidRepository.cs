using CarAuction.Persistence;
using Microsoft.EntityFrameworkCore;

public sealed class BidRepository : IBidRepository
{
    private readonly AppDbContext _context;

    public BidRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Bid bid)
    {
        await _context.Bids.AddAsync(bid);
    }

    public async Task<IReadOnlyList<Bid>> GetByAuctionIdAsync(Guid auctionId)
    {
        return await _context.Bids.Where(b => b.AuctionId == auctionId).ToListAsync();
    }
}