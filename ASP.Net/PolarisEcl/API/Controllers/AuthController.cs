using Microsoft.AspNetCore.Mvc;
using PolarisEcl.Application.Common.Interfaces;
using PolarisEcl.Application.Common.Dtos;
using PolarisEcl.Domain.Exceptions;
using FluentValidation;

namespace PolarisEcl.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto request, [FromServices] IValidator<RefreshTokenRequestDto> validator)
    {
        var validatorResult = await validator.ValidateAsync(request);

        if (!validatorResult.IsValid)
        {
            throw new BadRequestException("", validatorResult.ToDictionary());
        }
        LoginResponseDto response = await _authService.ValidateRefreshTokenAsync(request);

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request, [FromServices] IValidator<LoginRequestDto> validator)
    {
        var validatorResult = await validator.ValidateAsync(request);

        if (!validatorResult.IsValid)
        {
            throw new BadRequestException("", validatorResult.ToDictionary());
        }

        LoginResponseDto response = await _authService.LoginAsync(request);
        return Ok(response);
    }

    // task for forget password and reset password.
}