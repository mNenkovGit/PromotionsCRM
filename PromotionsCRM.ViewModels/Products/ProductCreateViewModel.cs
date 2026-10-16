using System.ComponentModel.DataAnnotations;
using static PromotionsCRM.Common.Validations;
namespace PromotionsCRM.ViewModels.Products
{
    public class ProductCreateViewModel
    {

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(ProductNameMaxLen, MinimumLength = ProductNameMinLen, ErrorMessage = "Product name must be between {2} and {1} characters long.")]
        public string Name { get; set; } = null!;


        [Range(1, int.MaxValue, ErrorMessage = "Invalid promotion!")]
        public int PromotionId { get; set; }

        public string? PromotionName { get; set; }
    }
}
