namespace PolarisEcl.Domain.Models;

public class RegressionParameter
{
    public Guid Id { get; set; }
    public decimal RSquareLimit { get; set; }
    public decimal PValueSelected { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedById { get; set; }
    public User UpdatedBy { get; set; } = null!;
}