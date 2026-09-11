using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Infrastructure.Data.Configurations;

public class ExpectedCorrelationConfig : IEntityTypeConfiguration<ExpectedCorrelation>
{
    public void Configure(EntityTypeBuilder<ExpectedCorrelation> builder)
    {
        builder.ToTable("ExpectedCorrelations");

        builder.HasKey(ec => ec.Id);

        builder.Property(ec => ec.PrimeLendingRate)
            .IsRequired();

        builder.Property(ec => ec.Inflation)
            .IsRequired();

        builder.Property(ec => ec.YieldOnTreasuryBills)
            .IsRequired();

        builder.Property(ec => ec.ExchangeRate)
            .IsRequired();

        builder.Property(ec => ec.MonetaryPolicyRate)
            .IsRequired();

        builder.Property(ec => ec.IsActive)
            .IsRequired();

        builder.Property(ec => ec.UpdatedAt)
            .IsRequired();

        builder.Property(ec => ec.UpdatedById)
            .IsRequired();

        builder.HasOne(ec => ec.UpdatedBy)
            .WithMany()
            .HasForeignKey(ec => ec.UpdatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(ec => ec.IsActive);
    }
}