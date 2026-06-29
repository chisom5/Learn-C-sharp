using PolarisEcl.Domain.Enums;

namespace PolarisEcl.Domain.Models;

public class EclDataSnapshot
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Year { get; set; }
    public int Month { get; set; }
    public string SnapshotDate { get; set; } = string.Empty;
    public ECLStage Stage { get; set; }

    public EclProductType ProductType { get; set; }

    // ECL model parameters
    public decimal PD { get; set; }
    public decimal LGD { get; set; }
    public decimal EAD { get; set; }
    public decimal ECL { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
