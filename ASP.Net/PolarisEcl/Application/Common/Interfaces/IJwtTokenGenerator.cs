using PolarisEcl.Domain.Models;

namespace PolarisEcl.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    (string, DateTime) GenerateToken(User user);
    (string, DateTime) GenerateRefreshToken();
}