using PolarisEcl.Application.Common.Dtos;

namespace PolarisEcl.Application.Common.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    Task<LoginResponseDto> ValidateRefreshTokenAsync(RefreshTokenRequestDto request);

    // Task <ResetPswdResponseDto> ResetPassword(RestPswdRequestDto request);
}