namespace PolarisEcl.Domain.Models;

public class CollateralType
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public decimal HaircutPercent { get; set; }
    public bool Perfected { get; set; }
    public bool IsActive { get; set; } = true;
    public int DurationYears { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedById { get; set; }
    public User UpdatedBy { get; set; } = null!;

}