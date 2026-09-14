using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PromotionsCRM.Common.Validations;
namespace PromotionsCRM.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ProductNameMaxlen)]
        public string Name { get; set; } = null!;

        [ForeignKey(nameof(Promotion))]
        public int PromotionId { get; set; }
        public virtual Promotion Promotion { get; set; } = null!;

        public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }
}
