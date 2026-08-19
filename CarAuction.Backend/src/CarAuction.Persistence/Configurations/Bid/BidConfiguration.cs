using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BidEntity = Bid;

namespace CarAuction.Persistence.Configurations.Auction;

public class BidConfiguration : IEntityTypeConfiguration<BidEntity>
{
    public void Configure(EntityTypeBuilder<BidEntity> builder)
    {
        builder.ToTable("Bids");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.AuctionId).IsRequired();
        builder.Property(b => b.BidderId).IsRequired();

        builder.Property(b => b.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(b => b.Time)
            .HasColumnType("timestamp without time zone")
            .IsRequired();

        builder.HasIndex(b => b.AuctionId);
    }
}