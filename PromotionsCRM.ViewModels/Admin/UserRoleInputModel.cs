using System.ComponentModel.DataAnnotations;

namespace PromotionsCRM.ViewModels.Admin
{
    public class UserRoleInputModel
    {
        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public string RoleName { get; set; } = null!;

    }
}
