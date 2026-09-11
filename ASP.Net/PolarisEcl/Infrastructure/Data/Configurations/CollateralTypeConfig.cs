using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Infrastructure.Data.Configurations;

public class CollateralTypeConfig : IEntityTypeConfiguration<CollateralType>
{
    public void Configure(EntityTypeBuilder<CollateralType> builder)
    {
        builder.ToTable("CollateralTypes");

        builder.HasKey(ct => ct.Id);

        builder.Property(ct => ct.Type)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(ct => ct.HaircutPercent)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(ct => ct.IsActive);

        builder.Property(ct => ct.Perfected)
            .IsRequired();

        builder.Property(ct => ct.DurationYears)
            .IsRequired();

        builder.Property(ct => ct.UpdatedAt)
            .IsRequired();

        builder.Property(ct => ct.UpdatedById)
            .IsRequired();

        builder.HasOne(ct => ct.UpdatedBy)
            .WithMany()
            .HasForeignKey(ct => ct.UpdatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(ct => ct.IsActive);
    }
}