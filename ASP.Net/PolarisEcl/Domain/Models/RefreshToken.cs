
namespace PolarisEcl.Domain.Models;

public class RefreshToken
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Token { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime Expires { get; set; }
    public DateTime? Revoked { get; set; }

    public string? ReplacedByToken { get; set; }
    public bool IsActive => Revoked is null && DateTime.UtcNow < Expires;
}