using System.ComponentModel.DataAnnotations;

namespace PromotionsCRM.Services.DTOs.UserManagement.Request
{
    public class UserRoleRequestDto
    {
        public string UserId { get; set; } = null!;

        public string RoleName { get; set; } = null!;
    }
}
