using Microsoft.EntityFrameworkCore;
using PolarisEcl.Application.Common.Interfaces;
using PolarisEcl.Application.Common.Dtos;
using PolarisEcl.Domain.Models;
using PolarisEcl.Domain.Exceptions;
using PolarisEcl.Application.Common;
using PolarisEcl.Application.Common.Wrappers;

namespace PolarisEcl.Application.Services;

public class UsersService : IUsersService
{
    private readonly IAppDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public UsersService(IAppDbContext context,
            IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> RegisterAsync(RegisterRequestDto request)
    {
        var userExist = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);

        if (userExist is not null)
        {
            throw new BadRequestException("Email already registered.");
        }

        string secureHash = _passwordHasher.HashPassword(request.Password);

        var newUser = new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = secureHash,
            Role = request.Role,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true,
            RefreshTokens = new List<RefreshToken>()
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return "Registration Completed.";
    }


    public async Task<PageResponse<AllUsersResponseDto>> GetAllUsers(PageQuery query)
    {
        var totalRecords = await _context.Users.CountAsync();

        var result = await _context.Users.OrderBy(d => d.Id).ApplyPagination(query.PageNum, query.PageSize).ToListAsync();

        var data = result.Select(user => new AllUsersResponseDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role,
            IsActive = user.IsActive,
            IsDeleted = user.IsDeleted,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        }).ToList();

        int totalPages = (int)Math.Ceiling((double)totalRecords / query.PageSize);

        return new PageResponse<AllUsersResponseDto>
        {
            Data = data,
            PageNum = query.PageNum,
            PageSize = query.PageSize,
            TotalPages = totalPages,
            TotalRecords = totalRecords
        };
    }

    public async Task<UpdateUserResponseDto> UpdateAUserAsync(Guid userId, UpdateUserRequestDto request)
    {

        var existingUser = await _context.Users.FindAsync(userId);
        if (existingUser is null)
        {
            throw new NotFoundException($"User with ID {userId} was not found.");
        }

        existingUser.FirstName = request.FirstName ?? existingUser.FirstName;
        existingUser.LastName = request.LastName ?? existingUser.LastName;
        existingUser.Email = request.Email ?? existingUser.Email;
        existingUser.Role = request.Role ?? existingUser.Role;
        existingUser.IsActive = request.IsActive ?? existingUser.IsActive;
        existingUser.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new UpdateUserResponseDto
        {
            FirstName = existingUser.FirstName,
            LastName = existingUser.LastName,
            Email = existingUser.Email,
            Role = existingUser.Role,
            IsActive = existingUser.IsActive,
            UpdatedAt = existingUser.UpdatedAt
        };
    }

    public async Task<string> DeActivateUserRoleAsync(Guid userId, Guid currentUserId)
    {
        if (userId == currentUserId)
        {
            throw new BadRequestException("Safety block: You cannot deactivate your own adminstrative account.");
        }
        var existingUser = await _context.Users.FindAsync(userId);
        if (existingUser is null)
        {
            throw new NotFoundException($"User not found.");
        }

        if (existingUser.Email == "admin@polarisecl.com")
        {
            throw new BadRequestException("Safety block: The system root account cannot be deactivated.");
        }
        existingUser.IsActive = !existingUser.IsActive;
        existingUser.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return !existingUser.IsActive ? "User Account Successfully Deactivated." : "User Account Successfully Activated";
    }

    public async Task<string> BulkDeleteUsersAsync(List<Guid> userIds, Guid currentUserId)
    {
        var safeUserIdsToDelete = await _context.Users
        .Where(d => userIds.Contains(d.Id) && d.Id != currentUserId && d.Email != "admin@polarisecl.com")
        .Select(u => u.Id).ToListAsync();

        if (safeUserIdsToDelete.Count == 0)
        {
            throw new ArgumentException("Operation cancelled. You cannot delete your own logged-in account.");
        }

        await _context.Users.Where(u => safeUserIdsToDelete.Contains(u.Id)).ExecuteUpdateAsync(d => d.SetProperty(u => u.IsDeleted, true));
        return $"{safeUserIdsToDelete.Count} User(s) Accounts Successfully Disabled.";
    }

    public async Task<string> DeleteAUserAsync(Guid userId, Guid currentUserId)
    {
        if (userId == currentUserId)
        {
            throw new BadRequestException("Safety block: You cannot disable your own adminstrative account.");
        }

        var existingUser = await _context.Users.FindAsync(userId);
        if (existingUser is null) throw new NotFoundException($"User with ID {userId} was not found.");

        if (existingUser.Email == "admin@polarisecl.com")
        {
            throw new BadRequestException("Safety block: The system root account cannot be disabled.");
        }

        existingUser.IsDeleted = true;
        existingUser.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return "User Account Successfully Disabled.";
    }


}