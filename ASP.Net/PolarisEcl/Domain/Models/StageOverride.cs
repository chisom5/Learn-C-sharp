using PolarisEcl.Domain.Enums;

namespace PolarisEcl.Domain.Models;

public class StageOverride
{
    public Guid Id { get; set; }
    public Guid LoanId { get; set; }
    public Loan Loan { get; set; } = null!;

    public Guid ComputationId { get; set; }
    public ECLComputation ECLComputation { get; set; } = null!;

    public ECLStage PreviousStage { get; set; }
    public ECLStage NewStage { get; set; }
    public string Reason { get; set; } = string.Empty;

    // Audit trail
    public Guid CreatedById { get; set; }
    public User CreatedBy { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}