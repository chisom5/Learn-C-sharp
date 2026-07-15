using PolarisEcl.Domain.Enums;
namespace PolarisEcl.Domain.Models;

public class ECLComputation
{
    public Guid Id { get; set; }
    public string ComputationName { get; set; } = string.Empty;
    public ComputationStatus Status { get; set; }
    public DateTime ReportingPeriod { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ArchivedAt { get; set; }
    public Guid ComputedById { get; set; }
    public User ComputedBy { get; set; } = null!;
    public Guid? AuthorizeById { get; set; }
    public User? AuthorizeBy { get; set; }
    public string? ReviewComment { get; private set; }
    public DateTime? ReviewedAt { get; set; }

    // model information
    public int PdWeightBaseline { get; set; }
    public int PdWeightBestcase { get; set; }
    public int PdWeightWorstcase { get; set; }
    public HistoricalMarginType HistoricalMargin { get; set; }
    public MacroeconomicAdjustmentFactorType MacroeconomicAdjustmentFactor { get; set; }

    public IReadOnlyCollection<ComputationFile> Files { get; set; } = []; //one-to-many
    public ECLReport? ECLReport { get; set; } //one-to-one relationship
    public ICollection<StageOverride> StageOverrides { get; set; } = [];
}