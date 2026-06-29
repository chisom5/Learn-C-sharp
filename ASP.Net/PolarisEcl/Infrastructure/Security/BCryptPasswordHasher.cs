using PolarisEcl.Application.Common.Interfaces;
using BCryptNet = BCrypt.Net.BCrypt;
namespace PolarisEcl.Infrastructure.Security;

public class BCryptPasswordHasher : IPasswordHasher
{

    public string HashPassword(string password)
    {
        return BCryptNet.HashPassword(password, workFactor: 12);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCryptNet.Verify(password, passwordHash);
    }
}