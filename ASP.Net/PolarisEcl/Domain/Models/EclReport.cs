using PolarisEcl.Domain.Enums;

namespace PolarisEcl.Domain.Models;

public class ECLReport
{
    public Guid Id { get; set; }
    public Guid ComputationId { get; set; }
    public ECLComputation ECLComputation { get; set; } = null!;
    public ReportReivew ReportStatus { get; set; }
    public decimal TotalECL { get; set; }
    public decimal TotalEAD { get; set; }
    public decimal TotalCoverageRatio { get; set; }
    public ICollection<EclReportRow> Rows { get; set; } = [];

}