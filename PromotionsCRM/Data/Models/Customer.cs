using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PromotionsCRM.Common.Validations;
namespace PromotionsCRM.Data.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(FirstNameMaxlen)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxlen)]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(EmailMaxLen)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(PhoneNumberLength)]
        [RegularExpression(PhoneNumberRegEx)]
        public string PhoneNumber { get; set; } = null!;

        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; } = null!;

        [Column(TypeName = "datetime2")]
        public DateTime RegisteredOn { get; set; }

        public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }
}
