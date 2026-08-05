using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientProfileEntity = ClientProfile;

namespace CarAuction.Persistence.Configurations.Identity;

public class ClientProfileConfiguration : IEntityTypeConfiguration<ClientProfileEntity>
{
    public void Configure(EntityTypeBuilder<ClientProfileEntity> builder)
    {
        builder.ToTable("ClientProfiles");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(c => c.LastName).IsRequired().HasMaxLength(100);
        builder.Property(c => c.BirthDate).IsRequired();

        builder.OwnsOne(c => c.Address, address =>
        {
            address.Property(a => a.StreetName).HasColumnName("StreetName").IsRequired().HasMaxLength(200);
            address.Property(a => a.HouseNumber).HasColumnName("HouseNumber").IsRequired().HasMaxLength(20);
            address.Property(a => a.PostalCode).HasColumnName("PostalCode").IsRequired().HasMaxLength(20);
            address.Property(a => a.City).HasColumnName("City").IsRequired().HasMaxLength(100);
            address.Property(a => a.Country).HasColumnName("Country").IsRequired().HasMaxLength(100);
        });
    }
}