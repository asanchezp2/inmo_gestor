using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateAPI.Domain.Entities;

namespace RealEstateAPI.Infrastructure.Configurations;

public class AdvisorConfiguration : IEntityTypeConfiguration<Advisor>
{
    public void Configure(EntityTypeBuilder<Advisor> builder)
    {
        builder.HasKey(a => a.AdvisorId);

        builder.Property(a => a.AdvisorId)
            .ValueGeneratedOnAdd();

        builder.Property(a => a.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Email)
            .HasMaxLength(100);

        builder.Property(a => a.PrimaryPhone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(a => a.SecondaryPhone)
            .HasMaxLength(20);

        builder.Property(a => a.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasMany(a => a.Properties)
            .WithOne(p => p.Advisor)
            .HasForeignKey(p => p.AdvisorId);
    }
}
