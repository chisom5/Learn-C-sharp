using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Infrastructure.Data.Configurations;

public class SegmentConfig : IEntityTypeConfiguration<Segment>
{
    public void Configure(EntityTypeBuilder<Segment> builder)
    {
        builder.ToTable("Segments");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(500);
            
        builder.Property(s => s.IsActive);

        builder.Property(s => s.UpdatedAt)
            .IsRequired();

        builder.Property(s => s.UpdatedById)
            .IsRequired();

        builder.HasOne(s => s.UpdatedBy)
            .WithMany()
            .HasForeignKey(s => s.UpdatedById)
            .OnDelete(DeleteBehavior.Restrict);
    }
}