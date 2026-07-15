using PolarisEcl.Domain.Enums;

namespace PolarisEcl.Domain.Models;

public class Loan
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Year { get; set; }
    public int Month { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public decimal OutstandingBalance { get; set; }
    public ECLStage Stage { get; set; }
    public EclProductType ProductType { get; set; }

    // ECL model parameters
    public decimal PD { get; set; }
    public decimal LGD { get; set; }
    public decimal EAD { get; set; }
    public decimal ECL { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<StageOverride> StageOverrides { get; set; } = [];
}
