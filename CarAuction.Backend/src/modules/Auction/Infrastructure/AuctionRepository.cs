using CarAuction.Persistence;
using Microsoft.EntityFrameworkCore;

public class AuctionRepository : IAuctionRepository
{
    private readonly AppDbContext _context;
    public AuctionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Auction auction)
    {
        await _context.Auctions.AddAsync(auction);
    }

    public async Task<bool> ExistsAsync(Guid auctionId)
    {
        return await _context.Auctions.AnyAsync(a => a.Id == auctionId);
    }

    public async Task<Auction?> GetByIdAsync(Guid auctionId)
    {
        return await _context.Auctions.FirstOrDefaultAsync(a => a.Id == auctionId);
    }
}