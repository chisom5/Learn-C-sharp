using Microsoft.EntityFrameworkCore;
using PolarisEcl.Application.Common.Interfaces;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Infrastructure.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options), IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken>  RefreshToken {get; set;}
    public DbSet<ECLComputation> ECLComputations { get; set; }
    public DbSet<ComputationFile> ComputationFile { get; set; }
    public DbSet<ECLReport> ECLReport { get; set; }
    public DbSet<EclReportRow> EclReportRow { get; set; }
    public DbSet<LGDResult> LGDResult { get; set; }
    public DbSet<PDResult> PDResults { get; set; }
    public DbSet<EADResult> EADResults { get; set; }
    public DbSet<EclDataSnapshot> EclDataSnapshot {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // apply all entity configurations 
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

    }

}