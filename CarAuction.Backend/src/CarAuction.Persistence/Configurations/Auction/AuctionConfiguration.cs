using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuctionEntity = Auction; // reasoning: causing naming conflicts (namespace vs class)

namespace CarAuction.Persistence.Configurations.Auction;

public class AuctionConfiguration : IEntityTypeConfiguration<AuctionEntity>
{
    public void Configure(EntityTypeBuilder<AuctionEntity> builder)
    {
        builder.ToTable("Auctions");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.StartTime)
            .HasColumnType("timestamp without time zone")
            .IsRequired();

        builder.Property(a => a.EndTime)
            .HasColumnType("timestamp without time zone")
            .IsRequired();

        builder.Property(a => a.CurrentPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(a => a.StartingPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(a => a.Status)
            .HasConversion<string>()
            .IsRequired();
    }
}