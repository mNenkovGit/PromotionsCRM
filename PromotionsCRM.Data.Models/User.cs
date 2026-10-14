using System.ComponentModel.DataAnnotations;
using static PromotionsCRM.Common.Validations;
namespace PromotionsCRM.Data.Models
{
    public class User
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
        [MaxLength(UsernameMaxlen)]
        public string UserName { get; set; } = null!;

        [Required]
        [MaxLength(PasswordMaxLen)]
        public string Password { get; set; } = null!;

        public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }
}
