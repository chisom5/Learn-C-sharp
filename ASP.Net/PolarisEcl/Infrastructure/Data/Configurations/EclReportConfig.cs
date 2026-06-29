using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Infrastructure.Data.Configurations;

public class ECLReportConfiguration : IEntityTypeConfiguration<ECLReport>
{
    public void Configure(EntityTypeBuilder<ECLReport> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.ReportStatus)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(r => r.TotalECL)
            .HasPrecision(18, 2);

        builder.Property(r => r.TotalEAD)
            .HasPrecision(18, 2);

        builder.Property(r => r.TotalCoverageRatio)
            .HasPrecision(18, 6); 

         builder.HasIndex(x => x.ComputationId).IsUnique();
    }
}