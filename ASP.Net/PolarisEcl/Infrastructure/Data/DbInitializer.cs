using Microsoft.EntityFrameworkCore;
using PolarisEcl.Application.Common.Interfaces;
using PolarisEcl.Domain.Models;
using PolarisEcl.Domain.Enums;

namespace PolarisEcl.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task SeedAdminUserAsync(IAppDbContext context, IPasswordHasher passwordHasher)
    {
        
        bool hasAdmin = await context.Users.AnyAsync(u => u.Role == UserRole.Admin);

        if (!hasAdmin)
        {
            var defaultAdmin = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "System",
                LastName = "Administrator",
                Email = "admin@polarisecl.com",
                PasswordHash = passwordHasher.HashPassword("Password@1!"), 
                Role = UserRole.Admin,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await context.Users.AddAsync(defaultAdmin);
            
            await context.SaveChangesAsync();
        }
    }

    // seed templates files for the two file type.
}