using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateAPI.Domain.Entities;

namespace RealEstateAPI.Infrastructure.Configurations;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.HasKey(p => p.PropertyId);

        builder.Property(p => p.PropertyId)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.PropertyCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(p => p.PropertyCode)
            .IsUnique();

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.Area)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(p => p.Address)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.ImageUrls)
            .HasMaxLength(2000);

        builder.Property(p => p.Type)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(p => p.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(p => p.Zone)
            .HasConversion<string>()
            .IsRequired();

        builder.HasOne(p => p.Advisor)
            .WithMany(a => a.Properties)
            .HasForeignKey(p => p.AdvisorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(p => p.Type);
        builder.HasIndex(p => p.Status);
        builder.HasIndex(p => p.Zone);
        builder.HasIndex(p => p.Price);
    }
}
