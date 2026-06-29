using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using PolarisEcl.Application.Common.Interfaces;
using PolarisEcl.Application.Common.Dtos;
using Microsoft.AspNetCore.Authorization;
using PolarisEcl.Domain.Exceptions;
using PolarisEcl.Filters;
using FluentValidation;

namespace PolarisEcl.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
[ServiceFilter(typeof(ActiveUserFilter))]
public class UserController : ControllerBase
{
    private readonly IUsersService _authService;

    public UserController(IUsersService authService)
    {
        _authService = authService;
    }


    [HttpGet("all")]
    public async Task<IActionResult> Users([FromQuery] PageQuery query)
    {

        var result = await _authService.GetAllUsers(query);
        return Ok(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterRequestDto request, [FromServices] IValidator<RegisterRequestDto> validator)
    {
        var validatorResult = await validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            throw new BadRequestException("", validatorResult.ToDictionary());
        }

        var result = await _authService.RegisterAsync(request);
        return Ok(new { message = result });
    }

    [HttpPatch("update/{userId:guid}")]
    public async Task<IActionResult> EditAUser([FromRoute] Guid userId, [FromBody] UpdateUserRequestDto request, [FromServices] IValidator<UpdateUserRequestDto> validator)
    {
        if (userId == Guid.Empty)
        {
            throw new BadRequestException("A valid user Id must be provided.");
        }

        var validatorResult = await validator.ValidateAsync(request);

        if (!validatorResult.IsValid)
        {
            throw new BadRequestException("", validatorResult.ToDictionary());
        }
        UpdateUserResponseDto response = await _authService.UpdateAUserAsync(userId, request);
        return Ok(response);
    }

    [HttpPatch("deactivate/{userId:guid}")]
    public async Task<IActionResult> DeactivateUser([FromRoute] Guid userId)
    {
        if (userId == Guid.Empty)
        {
            throw new BadRequestException("A valid user Id must be provided.");
        }

        Guid currentUserId = GenerateCurrentUserId();

        var response = await _authService.DeActivateUserRoleAsync(userId, currentUserId);
        return Ok(response);
    }

    [HttpDelete("delete/{userId:guid}")]
    public async Task<IActionResult> DeleteAUser([FromRoute] Guid userId)
    {

        if (userId == Guid.Empty)
        {
            throw new BadRequestException("A valid user Id must be provided.");
        }

        Guid currentUserId = GenerateCurrentUserId();

        var response = await _authService.DeleteAUserAsync(userId, currentUserId);
        return Ok(response);
    }

    [HttpDelete("bulk-delete")]
    public async Task<IActionResult> BulkDeleteUser([FromBody] BulkDeleteRequestDto request, IValidator<BulkDeleteRequestDto> validator)
    {
        var validatorResult = await validator.ValidateAsync(request);

        if (!validatorResult.IsValid)
        {
            throw new BadRequestException("", validatorResult.ToDictionary());
        }

        Guid currentUserId = GenerateCurrentUserId();

        try
        {
            var resultMessage = await _authService.BulkDeleteUsersAsync(request.userIds, currentUserId);
            return Ok(new { Message = resultMessage });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

    }

    private Guid GenerateCurrentUserId()
    {
        string? rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
        if (rawUserId == null || !Guid.TryParse(rawUserId, out Guid currentUserId))
        {
            throw new UnauthorizedException("User identity is missing or invalid.");
        }

        return currentUserId;
    }
}