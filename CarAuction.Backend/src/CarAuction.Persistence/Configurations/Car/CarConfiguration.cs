using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CarEntity = Car;

namespace CarAuction.Persistence.Configurations.Car;

public class CarConfiguration : IEntityTypeConfiguration<CarEntity>
{
    public void Configure(EntityTypeBuilder<CarEntity> builder)
    {
        builder.ToTable("Cars");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Brand).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Model).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Trim).HasMaxLength(100);
        builder.Property(c => c.Year).IsRequired();
        builder.Property(c => c.Kilometers).IsRequired();
        builder.Property(c => c.HasDamage).IsRequired();
        builder.Property(c => c.Description).HasMaxLength(2000);
        builder.Property(c => c.Color).HasMaxLength(50);
        builder.Property(c => c.Fuel).HasMaxLength(50);
        builder.Property(c => c.SellerId).IsRequired();
    }
}