using PromotionsCRM.Web.ViewModels.Promotions;

namespace PromotionsCRM.Web.ViewModels.Clients
{
    public class ClientDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Address { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string? Website { get; set; }

        public string Country{ get; set; } = null!;

        public string RegisteredOn { get; set; } = null!;

        public ICollection<PromotionViewModel> Promotions { get; set; } = new List<PromotionViewModel>();
    }
}
