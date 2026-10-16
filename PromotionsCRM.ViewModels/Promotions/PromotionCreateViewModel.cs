using PromotionsCRM.Common.Enums;
using System.ComponentModel.DataAnnotations;
using static PromotionsCRM.Common.Validations;
namespace PromotionsCRM.ViewModels.Promotions
{
    public class PromotionCreateViewModel
    {
        [Required(ErrorMessage = "Promotion name is required.")]
        [StringLength(PromotionNameMaxLen, MinimumLength = PromotionNameMinLen, ErrorMessage = "Promotion name must be between {2} and {1} characters long.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Please select a promotion type")]
        public PromotionType? PromotionType { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a client!")]
        public int ClientId { get; set; }

        public string? ClientName { get; set; }

    }

}
