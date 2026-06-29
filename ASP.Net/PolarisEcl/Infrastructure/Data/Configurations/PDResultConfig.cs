
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Infrastructure.Data.Configurations;

public class PDResultConfiguration : IEntityTypeConfiguration<PDResult>
{
    public void Configure(EntityTypeBuilder<PDResult> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.SectorName)
        .IsRequired()
        .HasMaxLength(150);

        builder.Property(l => l.Month1Value)
        .HasPrecision(18, 2);
        builder.Property(l => l.Month2Value)
                .HasPrecision(18, 2);
        builder.Property(l => l.Month3Value)
        .HasPrecision(18, 2);
        builder.Property(l => l.Month4Value)
        .HasPrecision(18, 2);
        builder.Property(l => l.Month5Value)
        .HasPrecision(18, 2);
        builder.Property(l => l.Month6Value)
        .HasPrecision(18, 2);
        builder.Property(l => l.Month7Value)
        .HasPrecision(18, 2);
        builder.Property(l => l.Month8Value)
        .HasPrecision(18, 2);
        builder.Property(l => l.Month9Value)
        .HasPrecision(18, 2);
        builder.Property(l => l.Month10Value)
        .HasPrecision(18, 2);
        builder.Property(l => l.Month11Value)
        .HasPrecision(18, 2);
        builder.Property(l => l.Month12Value)
                .HasPrecision(18, 2);
        // --- RELATIONSHIPS ---
        builder.HasOne(l => l.ECLComputation)
        .WithMany()
        .HasForeignKey(l => l.ComputationId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(l => l.ComputationId);

    }
}