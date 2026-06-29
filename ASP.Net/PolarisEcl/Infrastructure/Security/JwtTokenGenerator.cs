
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
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

    public JwtTokenGenerator(IOptions<JwtSettings> jwtOption)
    {
        _jwtSettings = jwtOption.Value;
    }

    public (string, DateTime) GenerateToken(User user)
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes);

        // here we build the claim. it is a piece of info that we store inside the jwt token
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim("FirstName", user.FirstName)
        };

        //create a security key from my secret string in jwtsettings.
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // metadata  property of the token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiresAt,
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = cred
        };

        // serialise how we want the token to look.
        var tokenHandler = new JsonWebTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return (token, expiresAt);
    }


    public (string, DateTime) GenerateRefreshToken()
    {
        var expiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenValidityInDays);

        var randomBytes = RandomNumberGenerator.GetBytes(64);
        
        string refreshToken =  Convert.ToBase64String(randomBytes);

        return (refreshToken, expiresAt);

    }

}