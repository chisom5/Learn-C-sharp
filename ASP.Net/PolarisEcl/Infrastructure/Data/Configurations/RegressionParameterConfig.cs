using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Infrastructure.Data.Configurations;

public class RegressionParameterConfig : IEntityTypeConfiguration<RegressionParameter>
{
    public void Configure(EntityTypeBuilder<RegressionParameter> builder)
    {
        builder.ToTable("RegressionParameters");

        builder.HasKey(rp => rp.Id);

        builder.Property(rp => rp.RSquareLimit)
            .IsRequired();

        builder.Property(rp => rp.PValueSelected)
            .IsRequired();

        builder.Property(rp => rp.IsActive)
       .IsRequired();

        builder.Property(rp => rp.UpdatedAt)
            .IsRequired();

        builder.Property(rp => rp.UpdatedById)
            .IsRequired();

        builder.HasOne(rp => rp.UpdatedBy)
            .WithMany()
            .HasForeignKey(rp => rp.UpdatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(rp => rp.IsActive);
        builder.HasQueryFilter(rp => rp.IsActive);
    }
}