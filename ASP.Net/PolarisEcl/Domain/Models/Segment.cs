namespace PolarisEcl.Domain.Models;

public class Segment
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedById { get; set; }
    public User UpdatedBy { get; set; } = null!;
}