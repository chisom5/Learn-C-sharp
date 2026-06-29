using PolarisEcl.Domain.Enums;

namespace PolarisEcl.Domain.Models;

public class EclReportRow
{
    public Guid Id { get; set; }
    public Guid ReportId { get; set; }
    public ECLReport ECLReport { get; set; } = null!;
    public ReportDimension Dimension { get; set; }  
    public string ProductLabel { get; set; } = string.Empty;  

    public long ECL { get; set; }
    public long EAD { get; set; }
    public decimal CoverageRatio { get; set; }

}