using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using static PromotionsCRM.Common.Validations;

namespace PromotionsCRM.ViewModels.Campaigns
{
    public class CampaignRegistrationViewModel
    {
        public int PromotionId { get; set; }

        [Required]
        [StringLength(FirstNameMaxLen, MinimumLength = FirstNameMinLen)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(LastNameMaxLen, MinimumLength = LastNameMinLen)]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(EmailMaxLen, MinimumLength = EmailMinLen)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(PhoneNumberLength)]
        [RegularExpression(PhoneNumberRegEx)]
        public string PhoneNumber { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "Please select a Country!")]
        public int CountryId { get; set; }

        public IEnumerable<SelectListItem>? Countries { get; set; } = new List<SelectListItem>();
        
        [Range(1, int.MaxValue, ErrorMessage = "Please select a Product!")]
        public int ProductId { get; set; }

        public IEnumerable<SelectListItem>? Products { get; set; } = new List<SelectListItem>();
       
        [Required]
        [DataType(DataType.Date)]
        public DateTime? PurchaseDate { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }

        
        [StringLength(PromotionNameMaxLen, MinimumLength = PromotionNameMinLen)]
        public string? PromotionName { get; set; }


    }
}
