using System.Security.Claims;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolarisEcl.Application.Common.Interfaces;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    protected readonly IAppDbContext Context;

    protected BaseApiController(IAppDbContext context)
    {
        Context = context;
    }

    // 💡 Shortcut property to grab just the Guid ID without hitting the database
    protected Guid CurrentUserId
    {
        get
        {
            string? rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");

            if (string.IsNullOrEmpty(rawUserId) || !Guid.TryParse(rawUserId, out Guid userId))
            {

                throw new UnauthorizedAccessException("User identity is missing or invalid.");
            }
            return userId;
        }
    }

    // Shared helper method to retrieve the fully loaded User entity from DB
    protected async Task<User> GetCurrentUserAsync()
    {
        var userId = CurrentUserId;
        var user = await Context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            throw new UnauthorizedAccessException("The authenticated user record could not be found.");
        }

        return user;
    }
}