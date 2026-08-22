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

    public async Task<IEnumerable<Auction>> GetAllAuctionsAsync()
    {
        return await _context.Auctions.ToListAsync();
    }

    public async Task<IReadOnlyList<Auction>> GetAuctionsToActivateAsync()
    {
        // Value converters only apply to mapped entity properties, not ad-hoc query parameters
        // DateTime.UtcNow must be re-tagged Unspecified here to match the timestamp-without-timezone column.
        var now = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
        var result = await _context.Auctions.Where(a => a.Status == AuctionStatus.Scheduled && now >= a.StartTime).ToListAsync();

        // var test = await _context.Auctions.Where(a => a.Status == AuctionStatus.Scheduled).ToListAsync();
        // var time1 = test[0].StartTime;
        // var timeNow = DateTime.Now >= time1;

        return result;
    }

    public async Task<IReadOnlyList<Auction>> GetAuctionsToCloseAsync()
    {
        var now = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
        return await _context.Auctions.Where(a => a.Status == AuctionStatus.Active && now >= a.EndTime).ToListAsync();
    }

    public async Task<Auction?> GetByIdAsync(Guid auctionId)
    {
        return await _context.Auctions.FirstOrDefaultAsync(a => a.Id == auctionId);
    }
}