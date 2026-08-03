
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PolarisEcl.Application.Common.Interfaces;
using PolarisEcl.Application.Common.Security;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Infrastructure.Security;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;
    private readonly ILogger<JwtTokenGenerator> _logger;

    public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings, ILogger<JwtTokenGenerator> logger)
    {
        _jwtSettings = jwtSettings?.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
        _logger = logger;
    }


    public (string, DateTime) GenerateToken(User user)
    {
        _logger.LogInformation($"ExpiryInMinutes setting value: {_jwtSettings.ExpiryInMinutes}");

        var expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes);

        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role.ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim("FirstName", user.FirstName)
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiresAt,
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = cred
        };

        var tokenHandler = new JsonWebTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return (token, expiresAt);
    }

    public (string, DateTime) GenerateRefreshToken()
    {
        var expiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenValidityInDays);

        var randomBytes = RandomNumberGenerator.GetBytes(64);

        string refreshToken = Convert.ToBase64String(randomBytes);

        return (refreshToken, expiresAt);

    }

}