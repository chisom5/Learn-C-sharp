
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Infrastructure.Data.Configurations;

public class LGDResultConfiguration : IEntityTypeConfiguration<LGDResult>
{
    public void Configure(EntityTypeBuilder<LGDResult> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.SectorName)
        .IsRequired()
        .HasMaxLength(150);

        builder.Property(l => l.LgdValue)
        .HasPrecision(18, 2);

        // --- RELATIONSHIPS ---
        builder.HasOne(l => l.ECLComputation)
        .WithMany()
        .HasForeignKey(l => l.ComputationId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(l => l.ComputationId);
    }
}