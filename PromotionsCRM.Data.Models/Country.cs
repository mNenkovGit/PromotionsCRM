using System.ComponentModel.DataAnnotations;
using static PromotionsCRM.Common.Validations;

namespace PromotionsCRM.Data.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CountryNameMaxLen)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

        public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

        public virtual ICollection<PromotionCountry> PromotionsCountries { get; set; } = new List<PromotionCountry>();
    }
}
