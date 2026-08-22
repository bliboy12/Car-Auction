using CarAuction.Persistence;
using Microsoft.EntityFrameworkCore;

public sealed class ClientProfileRepository : IClientProfileRepository
{
    private readonly AppDbContext _context;

    public ClientProfileRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(ClientProfile client)
    {
        await _context.ClientProfiles.AddAsync(client);
    }

    public async Task<ClientProfile?> GetByIdAsync(Guid userId)
    {
        return await _context.ClientProfiles.FirstOrDefaultAsync(u => u.Id == userId);
    }
}