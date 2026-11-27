using PromotionsCRM.Services.DTOs.UserManagement.Request;
using PromotionsCRM.Services.DTOs.UserManagement.Response;

namespace PromotionsCRM.Services.Interfaces
{
    public interface IUserManagementService
    {
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();

        Task<bool> AssignRoleAsync(UserRoleRequestDto request, string currentUserId);

        Task<bool> RemoveRoleAsync(UserRoleRequestDto request, string currentUserId);

        Task<bool> DeleteUserAsync(string userId, string currentUserId);

    }
}
