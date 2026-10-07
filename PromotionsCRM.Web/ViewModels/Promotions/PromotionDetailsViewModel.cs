
namespace PromotionsCRM.Web.ViewModels.Promotions
{
    public class PromotionDetailsViewModel : PromotionViewModel
    {
        public int ClientId { get; set; }

        public ICollection<string> Countries { get; set; } = new List<string>();

        public ICollection<string> Products { get; set; } = new List<string>();
    }
}
