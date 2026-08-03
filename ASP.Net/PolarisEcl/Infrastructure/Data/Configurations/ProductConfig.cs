using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Infrastruture.Data.Configurations;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.SegmentType)
      .IsRequired()
      .HasMaxLength(500);

        builder.Property(p => p.IsActive)
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedById)
            .IsRequired();

        builder.HasOne(p => p.UpdatedBy)
            .WithMany()
            .HasForeignKey(p => p.UpdatedById)
            .OnDelete(DeleteBehavior.Restrict);
    }
}