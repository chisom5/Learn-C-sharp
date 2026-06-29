using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Infrastructure.Data.Configurations;

public class EclReportRowConfiguration : IEntityTypeConfiguration<EclReportRow>
{
    public void Configure(EntityTypeBuilder<EclReportRow> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.ProductLabel)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(r => r.Dimension)
            .HasConversion<string>()
            .HasMaxLength(50);

        // Long mappings (bigint) don't need scale/precision, but decimals do!
        builder.Property(r => r.CoverageRatio)
            .HasPrecision(18, 6); 

        // 3. Relationships (The Many-to-One side)
        builder.HasOne(r => r.ECLReport)
            .WithMany(report => report.Rows) 
            .HasForeignKey(r => r.ReportId)
            .OnDelete(DeleteBehavior.Cascade); 
            
        // 4. Performance Optimization: Indexing
        // Since you'll almost always query rows by their ReportId to display them in a grid,
        // a database index on ReportId makes lookups lightning fast.
        builder.HasIndex(r => r.ReportId);
    }
}