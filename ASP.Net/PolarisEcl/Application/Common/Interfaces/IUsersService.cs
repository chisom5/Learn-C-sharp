using PolarisEcl.Application.Common.Dtos;
using PolarisEcl.Application.Common.Wrappers;

namespace PolarisEcl.Application.Common.Interfaces;

public interface IUsersService
{
    Task<string> RegisterAsync(RegisterRequestDto request);
    Task<UpdateUserResponseDto> UpdateAUserAsync(Guid userId, UpdateUserRequestDto request);
    Task<string> DeActivateUserRoleAsync(Guid userId, Guid currentUserId);
    Task<string> BulkDeleteUsersAsync(List<Guid> userIds, Guid currentUserId);
    Task<string> DeleteAUserAsync(Guid userId, Guid currentUserId);
    Task<PageResponse<AllUsersResponseDto>> GetAllUsers(PageQuery query);
}