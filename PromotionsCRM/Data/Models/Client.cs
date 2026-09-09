using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PromotionsCRM.Common.Validations;
namespace PromotionsCRM.Data.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ClientNameMaxLen)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(EmailMaxLen)]
        public string Email { get; set; } = null!;

        [MaxLength(AddressMaxLen)]
        public string? Address { get; set; }

        [Required]
        [MaxLength(PhoneNumberLength)]
        [RegularExpression(Common.Validations.PhoneNumberRegEx)]
        public string PhoneNumber { get; set; } = null!;

        [MaxLength(WebsiteMaxLen)]
        public string? Website { get; set; }

        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; } = null!;

        [Column(TypeName = "datetime2")]
        public DateTime RegisteredOn { get; set; }

        public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();
    }
}
