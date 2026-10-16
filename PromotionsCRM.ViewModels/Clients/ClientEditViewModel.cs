using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using static PromotionsCRM.Common.Validations;

namespace PromotionsCRM.ViewModels.Clients
{
    public class ClientEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Client Name is Mandatory!")]
        [StringLength(ClientNameMaxLen, MinimumLength = ClientNameMinLen, ErrorMessage = "Client name must be between {2} and {1} characters!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email Address is Mandatory!")]
        [StringLength(EmailMaxLen, MinimumLength = EmailMinLen, ErrorMessage = "E-mail address must be between {2} and {1} characters!")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [StringLength(AddressMaxLen, MinimumLength = AddressMinLen, ErrorMessage = "Address must be between {2} and {1} characters!")]
        public string? Address { get; set; }

        [Required]
        [RegularExpression(PhoneNumberRegEx, ErrorMessage = "Phone Number must match the following pattern \"+359888123456\" (plus sign followed by exactly 12 digits")]
        [StringLength(PhoneNumberLength)]
        public string PhoneNumber { get; set; } = null!;

        [StringLength(WebsiteMaxLen, MinimumLength = WebsiteMinLen, ErrorMessage = "Website Url must be between {2} and {1} characters!")]
        public string? Website { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a country!")]
        public int CountryId { get; set; }

        public IEnumerable<SelectListItem>? Countries { get; set; }
    }
}
