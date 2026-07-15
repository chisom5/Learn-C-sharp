using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Infrastructure.Data.Configurations;

public class StageOverrideConfiguration : IEntityTypeConfiguration<StageOverride>

{
    public void Configure(EntityTypeBuilder<StageOverride> builder)
    {
        builder.ToTable("StageOverrides");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PreviousStage)
        .IsRequired();

        builder.Property(x => x.NewStage)
        .IsRequired();

        builder.Property(x => x.Reason)
        .IsRequired();

        // Audit trail
        builder.Property(x => x.CreatedById)
                    .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");


        builder.HasOne(x => x.CreatedBy)
                    .WithMany() // Leaves the collection side empty if User doesn't have a StageOverrides list
                    .HasForeignKey(x => x.CreatedById)
                    .OnDelete(DeleteBehavior.Restrict);
    }
}