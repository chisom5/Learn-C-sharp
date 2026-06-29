namespace PolarisEcl.Domain.Models;

public class LGDResult
{
    public Guid Id { get; set; }
    public Guid ComputationId { get; set; }
    public ECLComputation ECLComputation { get; set; } = null!;
    public string SectorName { get; set; } = string.Empty;
    public decimal LgdValue { get; set; }
}