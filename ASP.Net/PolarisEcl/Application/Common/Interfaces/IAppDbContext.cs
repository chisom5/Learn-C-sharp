using Microsoft.EntityFrameworkCore;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Application.Common.Interfaces;

public interface IAppDbContext
{

    DbSet<User> Users { get; set; }
    DbSet<RefreshToken> RefreshToken { get; set; }
    DbSet<ECLComputation> ECLComputations { get; set; }
    DbSet<ComputationFile> ComputationFiles { get; set; }
    DbSet<ECLReport> ECLReports { get; set; }
    DbSet<EclReportRow> EclReportRows { get; set; }
    DbSet<LGDResult> LGDResults { get; set; }
    DbSet<PDResult> PDResults { get; set; }
    DbSet<EADResult> EADResults { get; set; }
    DbSet<Loan> Loans { get; set; }
    DbSet<StageOverride> StageOverrides { get; set; }
    DbSet<Segment> Segments { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<RegressionParameter> RegressionParameters{ get; set; }
    DbSet<ExpectedCorrelation> ExpectedCorrelations { get; set; }
    DbSet<CollateralType> CollateralTypes { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

