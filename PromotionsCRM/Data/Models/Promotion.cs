using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PromotionsCRM.Common.Validations;
namespace PromotionsCRM.Data.Models
{
    public class Promotion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(PromotionNameMaxlen)]
        public string Name { get; set; } = null!;

        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime EndDate { get; set; }

        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }
        public virtual Client Client { get; set; } = null!;

        public virtual ICollection<PromotionCountry> PromotionsCountries { get; set; } = new List<PromotionCountry>();

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        public virtual ICollection<Submission> Submissions { get; set; }  = new List<Submission>();
    }
}
