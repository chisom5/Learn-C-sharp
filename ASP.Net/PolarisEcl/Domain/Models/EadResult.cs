namespace PolarisEcl.Domain.Models;

public class EADResult
{
    public Guid Id { get; set; }
    public Guid ComputationId { get; set; }
    public ECLComputation ECLComputation { get; set; } = null!;
    public string AccountNo { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string Products { get; set; } = string.Empty;
    public decimal AmortizedCost { get; set; }
    public string LoanType { get; set; } = string.Empty;
    public int Dpd { get; set; } // Days Past Due
    public decimal EIROrCommercialRate { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly MaturityDate { get; set; }
    public decimal SavingsCollateral { get; set; }
    // public decimal CarryingAmount { get; set; }
    public decimal Month1Value { get; set; }
    public decimal Month2Value { get; set; }
    public decimal Month3Value { get; set; }
    public decimal Month4Value { get; set; }
    public decimal Month5Value { get; set; }
    public decimal Month6Value { get; set; }
    public decimal Month7Value { get; set; }
    public decimal Month8Value { get; set; }
    public decimal Month9Value { get; set; }
    public decimal Month10Value { get; set; }
    public decimal Month11Value { get; set; }
    public decimal Month12Value { get; set; }
}