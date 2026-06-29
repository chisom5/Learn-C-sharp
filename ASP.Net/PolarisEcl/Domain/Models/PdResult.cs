namespace PolarisEcl.Domain.Models;

public class PDResult
{
    public Guid Id { get; set; }
    public Guid ComputationId { get; set; }
    public ECLComputation ECLComputation { get; set; } = null!;
    public string SectorName { get; set; } = string.Empty;
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