
using PromotionsCRM.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromotionsCRM.Data.Models
{
    public class Submission
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = null!;

        [ForeignKey(nameof(Promotion))]
        public int PromotionId { get; set; }
        public virtual Promotion Promotion { get; set; } = null!;

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;

        public StatusName Status { get; set; }


        [ForeignKey(nameof(User))]
        public int? UserId { get; set; }
        public virtual User? User { get; set; } 


        [Column(TypeName = "datetime2")]
        public DateTime PurchaseDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime SubmittedOn { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ProcessedOn { get; set; }
    }
}
