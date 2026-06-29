

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Infrastructure.Data.Configurations;

public class EclComputationConfiguration : IEntityTypeConfiguration<ECLComputation>
{
    public void Configure(EntityTypeBuilder<ECLComputation> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ReportingPeriod)
              .IsRequired();

        builder.Property(c => c.Status)
             .HasConversion<string>()
             .HasMaxLength(30);

        builder.Property(c => c.HistoricalMargin)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(c => c.MacroeconomicAdjustmentFactor)
            .HasConversion<string>()
            .HasMaxLength(50);

        // --- RELATIONSHIPS ---

        builder.HasOne(c => c.ComputedBy)
        .WithMany()
        .HasForeignKey(x => x.ComputedById)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.AuthorizeBy)
        .WithMany()
        .HasForeignKey(x => x.AuthorizeById)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.ECLReport)
        .WithOne(c=> c.ECLComputation)
        .HasForeignKey<ECLReport>(c => c.ComputationId);
    }
}

