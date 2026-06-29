using Microsoft.EntityFrameworkCore;
using PolarisEcl.Application.Common.Interfaces;
using PolarisEcl.Application.Common.Dtos;
using PolarisEcl.Domain.Models;
using PolarisEcl.Domain.Enums;
using PolarisEcl.Domain.Exceptions;

namespace PolarisEcl.Application.Services;

public class AuthService : IAuthService
{
    private readonly IAppDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(IAppDbContext context,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);

        if (user is null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedException("Invalid Email or password");
        }

        if (!user.IsActive)
        {
            throw new UnauthorizedException("The Account is deactivated.");
        }

        (string token, DateTime expiresAt) = _jwtTokenGenerator.GenerateToken(user);
        (string refreshToken, DateTime refreshExpiry) = _jwtTokenGenerator.GenerateRefreshToken();

        var dbRecord = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = refreshToken,
            UserId = user.Id,
            Expires = refreshExpiry,
            CreatedAt = DateTime.UtcNow
        };

        _context.RefreshToken.Add(dbRecord);
        await _context.SaveChangesAsync();

        return new LoginResponseDto
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = user.Role,
            IsActive = user.IsActive,
            AccessToken = token,
            ExpiresIn = expiresAt,
            RefreshToken = refreshToken
        };
    }

    public async Task<LoginResponseDto> ValidateRefreshTokenAsync(RefreshTokenRequestDto request)
    {
        var storedToken = await _context.RefreshToken.Include(t => t.User).SingleOrDefaultAsync(t => t.Token == request.Token);

        if (storedToken is null || !storedToken.IsActive)
        {
            throw new UnauthorizedException("Invalid or expired session. Please log in again.");
        }

        var user = storedToken.User;

        if (user == null || !user.IsActive || user.IsDeleted)
        {
            storedToken.Revoked = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            throw new UnauthorizedException("Access denied. Your account has been deactivated or disabled.");
        }

        storedToken.Revoked = DateTime.UtcNow;

        (string newAccessToken, DateTime expiresAt) = _jwtTokenGenerator.GenerateToken(storedToken.User);
        (string newRefreshTokenString, DateTime refreshExpiry) = _jwtTokenGenerator.GenerateRefreshToken();

        storedToken.ReplacedByToken = newRefreshTokenString;

        var newdbRecord = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = newRefreshTokenString,
            UserId = storedToken.UserId,
            Expires = refreshExpiry,
            CreatedAt = DateTime.UtcNow
        };

        _context.RefreshToken.Add(newdbRecord);
        await _context.SaveChangesAsync();

        return new LoginResponseDto
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = user.Role,
            IsActive = user.IsActive,
            AccessToken = newAccessToken,
            ExpiresIn = expiresAt,
            RefreshToken = newRefreshTokenString
        };
    }
}