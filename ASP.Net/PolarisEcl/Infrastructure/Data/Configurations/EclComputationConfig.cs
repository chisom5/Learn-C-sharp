using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Infrastructure.Data.Configurations;

public class EclComputationConfiguration : IEntityTypeConfiguration<ECLComputation>
{
    public void Configure(EntityTypeBuilder<ECLComputation> builder)
    {
        builder.ToTable("ECLComputations");
        
        builder.HasKey(x => x.Id);

        // --- Core Text Properties ---
        builder.Property(x => x.ComputationName)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(x => x.ReviewComment)
            .IsRequired(false) 
            .HasMaxLength(1000); 

        // --- Status and Enums ---
        builder.Property(c => c.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(c => c.HistoricalMargin)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(c => c.MacroeconomicAdjustmentFactor)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        // --- Financial Weights ---
        builder.Property(c => c.PdWeightBaseline).IsRequired().HasDefaultValue(0);
        builder.Property(c => c.PdWeightBestcase).IsRequired().HasDefaultValue(0);
        builder.Property(c => c.PdWeightWorstcase).IsRequired().HasDefaultValue(0);

        // --- Dates & Audit Logs ---
        builder.Property(x => x.ReportingPeriod)
            .IsRequired(); 

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP"); 

        builder.Property(x => x.ReviewedAt)
            .IsRequired(false);

        builder.Property(x => x.ArchivedAt)
            .IsRequired(false); 

        // --- RELATIONSHIPS ---

        // 1. Audit Users
        builder.HasOne(c => c.ComputedBy)
            .WithMany()
            .HasForeignKey(x => x.ComputedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.AuthorizeBy)
            .WithMany()
            .HasForeignKey(x => x.AuthorizeById)
            .OnDelete(DeleteBehavior.Restrict);

        // 2. One-to-One: ECLComputation <-> ECLReport
        builder.HasOne(c => c.ECLReport)
            .WithOne(r => r.ECLComputation)
            .HasForeignKey<ECLReport>(r => r.ComputationId)
            .OnDelete(DeleteBehavior.Cascade); // If calculation batch goes, drop the report output cleanly

        // 3. One-to-Many: StageOverrides
        builder.HasMany(c => c.StageOverrides)
            .WithOne(o => o.ECLComputation)
            .HasForeignKey(o => o.ComputationId)
            .OnDelete(DeleteBehavior.Cascade);

        // --- PERFORMANCE INDEXES ---
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.ReportingPeriod);
    }
}