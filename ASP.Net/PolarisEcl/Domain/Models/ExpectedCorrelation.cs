using PolarisEcl.Domain.Enums;

namespace PolarisEcl.Domain.Models;

public class ExpectedCorrelation
{
    public Guid Id { get; set; }
    public CorrelationDirection PrimeLendingRate { get; set; }
    public CorrelationDirection Inflation { get; set; }
    public CorrelationDirection YieldOnTreasuryBills { get; set; }
    public CorrelationDirection ExchangeRate { get; set; }
    public CorrelationDirection MonetaryPolicyRate { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedById { get; set; }
    public User UpdatedBy { get; set; } = null!;
}