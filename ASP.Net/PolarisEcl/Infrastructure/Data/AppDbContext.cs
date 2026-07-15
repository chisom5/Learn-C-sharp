using Microsoft.EntityFrameworkCore;
using PolarisEcl.Application.Common.Interfaces;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Infrastructure.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options), IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshToken { get; set; }
    public DbSet<ECLComputation> ECLComputations { get; set; }
    public DbSet<ComputationFile> ComputationFiles { get; set; }
    public DbSet<ECLReport> ECLReports { get; set; }
    public DbSet<EclReportRow> EclReportRows { get; set; }
    public DbSet<LGDResult> LGDResults { get; set; }
    public DbSet<PDResult> PDResults { get; set; }
    public DbSet<EADResult> EADResults { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<StageOverride> StageOverrides { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

    }

}