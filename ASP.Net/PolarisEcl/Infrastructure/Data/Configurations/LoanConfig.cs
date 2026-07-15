using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Infrastructure.Data.Configurations;

public class LoanConfiguration : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.ToTable("Loans");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Year)
        .IsRequired()
        .HasDefaultValue(0);

        builder.Property(x => x.Month)
       .IsRequired()
       .HasDefaultValue(0);

        builder.Property(x => x.AccountNumber)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(x => x.CustomerName)
       .IsRequired()
       .HasMaxLength(250);

        builder.Property(x => x.OutstandingBalance)
        .IsRequired();

        builder.Property(x => x.Stage)
      .IsRequired();

        builder.Property(x => x.ProductType)
      .IsRequired()
      .HasConversion<string>();

        builder.Property(x => x.PD)
       .IsRequired()
       .HasPrecision(18, 6);

        builder.Property(x => x.LGD)
        .IsRequired()
        .HasPrecision(18, 6);

        builder.Property(x => x.EAD)
        .IsRequired()
        .HasPrecision(18, 2);

        builder.Property(x => x.ECL)
        .IsRequired()
        .HasPrecision(18, 2);

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasMany(x => x.StageOverrides)
        .WithOne(x => x.Loan)
        .HasForeignKey(x => x.LoanId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}