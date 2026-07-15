using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Infrastructure.Data.Configurations;

public class ComputationFileConfiguration : IEntityTypeConfiguration<ComputationFile>
{
    public void Configure(EntityTypeBuilder<ComputationFile> builder)
    {
        builder.ToTable("ComputationFiles");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.FileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(f => f.StoragePath)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(f => f.File)
        .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(f => f.UploadedAt)
        .IsRequired()
        .HasDefaultValueSql("CURRENT_TIMESTAMP"); 

        builder.Property(f => f.UploadedById)
            .IsRequired();

        builder.Property(f => f.ComputationId)
            .IsRequired(false);

        // --- RELATIONSHIPS ---

        // 1. One-to-Many: ECLComputation -> ComputationFiles
        builder.HasOne(f => f.ECLComputation)
            .WithMany(c => c.Files)
            .HasForeignKey(f => f.ComputationId)
            .OnDelete(DeleteBehavior.Cascade); // If computation is deleted, delete its file references

        // 2. Relationship: UploadedBy (User)
        builder.HasOne(f => f.UploadedBy)
            .WithMany()
            .HasForeignKey(f => f.UploadedById)
            .OnDelete(DeleteBehavior.Restrict);
    }
}