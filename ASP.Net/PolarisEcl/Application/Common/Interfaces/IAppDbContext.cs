using Microsoft.EntityFrameworkCore;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Application.Common.Interfaces;

public interface IAppDbContext
{

    DbSet<User> Users { get; set; }
    DbSet<RefreshToken> RefreshToken { get; set; }
    DbSet<ECLComputation> ECLComputations { get; set; }
    DbSet<ComputationFile> ComputationFile { get; set; }
    DbSet<ECLReport> ECLReport { get; set; }
    DbSet<EclReportRow> EclReportRow { get; set; }
    DbSet<LGDResult> LGDResult { get; set; }
    DbSet<PDResult> PDResults { get; set; }
    DbSet<EADResult> EADResults { get; set; }
    public DbSet<EclDataSnapshot> EclDataSnapshot {get; set;}

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

