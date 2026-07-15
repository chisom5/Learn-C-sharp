using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Infrastructure.Data.Configurations;

public class EADResultConfiguration : IEntityTypeConfiguration<EADResult>
{
    public void Configure(EntityTypeBuilder<EADResult> builder)
    {
        builder.ToTable("EADResults");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.AccountNo)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.CustomerName)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(e => e.Products)
        .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.LoanType)
        .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.AmortizedCost)
        .IsRequired()
            .HasPrecision(18, 4);

        builder.Property(e => e.Dpd)
                    .IsRequired()
                    .HasDefaultValue(0);

        builder.Property(e => e.StartDate)
        .IsRequired();

        builder.Property(e => e.MaturityDate)
            .IsRequired();

        builder.Property(e => e.EIROrCommercialRate)
            .IsRequired()
            .HasPrecision(18, 6);

        builder.Property(e => e.SavingsCollateral)
            .IsRequired()
            .HasPrecision(18, 4);

        // 4. Time-Series Columns (Month 1 to Month 12)
        // Grouping them tightly with identical scale configurations for consistent reporting metrics
        builder.Property(e => e.Month1Value).HasPrecision(18, 4);
        builder.Property(e => e.Month2Value).HasPrecision(18, 4);
        builder.Property(e => e.Month3Value).HasPrecision(18, 4);
        builder.Property(e => e.Month4Value).HasPrecision(18, 4);
        builder.Property(e => e.Month5Value).HasPrecision(18, 4);
        builder.Property(e => e.Month6Value).HasPrecision(18, 4);
        builder.Property(e => e.Month7Value).HasPrecision(18, 4);
        builder.Property(e => e.Month8Value).HasPrecision(18, 4);
        builder.Property(e => e.Month9Value).HasPrecision(18, 4);
        builder.Property(e => e.Month10Value).HasPrecision(18, 4);
        builder.Property(e => e.Month11Value).HasPrecision(18, 4);
        builder.Property(e => e.Month12Value).HasPrecision(18, 4);

        // 5. Relationship (Child side mapping to the Parent ECLComputation)
        builder.HasOne(e => e.ECLComputation)
            .WithMany() // Assuming ECLComputation has an ICollection<EADResult> or you can use .WithMany(c => c.EadResults) if you added the collection property
            .HasForeignKey(e => e.ComputationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => new { e.ComputationId, e.AccountNo });
    }
}