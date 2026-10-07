using Microsoft.AspNetCore.Mvc.Rendering;
using PromotionsCRM.Web.Data.Enums;
using System.ComponentModel.DataAnnotations;
using static PromotionsCRM.Common.Validations;
namespace PromotionsCRM.Web.ViewModels.Promotions
{
    public class PromotionEditViewModel
    {
        public int Id { get; internal set; }
        
        [Required(ErrorMessage = "Promotion name is required.")]
        [StringLength(PromotionNameMaxlen, MinimumLength = PromotionNameMinLen, ErrorMessage = "Promotion name must be between {2} and {1} characters long.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Please select a promotion type")]
        public PromotionType? PromotionType { get; set; }
    }
    
}
