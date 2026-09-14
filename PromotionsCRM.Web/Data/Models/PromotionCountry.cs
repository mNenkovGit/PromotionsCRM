
using System.ComponentModel.DataAnnotations.Schema;

namespace PromotionsCRM.Data.Models
{
    public class PromotionCountry
    {
        [ForeignKey(nameof(Promotion))]
        public int PromotionId { get; set; }

        public virtual Promotion Promotion { get; set; } = null!;

        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; } = null!;
    }
}
