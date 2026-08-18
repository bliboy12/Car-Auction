using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.Persistence;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Auction> Auctions => Set<Auction>();
    public DbSet<Car> Cars => Set<Car>();
    public DbSet<Bid> Bids => Set<Bid>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<ClientProfile> ClientProfiles => Set<ClientProfile>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    // every DateTime your C# code ever sees from the database is explicitly tagged Kind = Utc
    // removing the ambiguity at its source, once, for your entire codebase, rather than trusting every future query to handle it correctly on its own.
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTime>().HaveConversion<UtcDateTimeConverter>();
    }
}