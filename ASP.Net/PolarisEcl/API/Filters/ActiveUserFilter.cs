using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PolarisEcl.Application.Common.Interfaces;


namespace PolarisEcl.Filters;

public class ActiveUserFilter : IAsyncAuthorizationFilter
{

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (user?.Identity == null || !user.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedObjectResult(new { message = "You are not authenticated." });
            return;
        }

        var rawUserId = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? user.FindFirstValue("sub");

        if (string.IsNullOrEmpty(rawUserId) || !Guid.TryParse(rawUserId, out Guid userId))
        {
            context.Result = new UnauthorizedObjectResult(new { message = "Invalid user identity token." });
            return;
        }

        var dbContext = context.HttpContext.RequestServices.GetRequiredService<IAppDbContext>();

        var dbUser = await dbContext.Users.FindAsync(userId);
        
        if (dbUser == null)
        {
            context.Result = new UnauthorizedObjectResult(new { message = "User record no longer exist." });
            return;
        }


        if (!dbUser.IsActive)
        {
            context.Result = new ObjectResult(new { message = "Unauthorized: This administrator account has been deactivated." })
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
            return;
        }

        if (dbUser.IsDeleted)
        {
            context.Result = new ObjectResult(new { message = "Unauthorized: This administrator account has been disabled." })
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
            return;
        }
    }
}