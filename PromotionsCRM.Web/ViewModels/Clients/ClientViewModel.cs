using PromotionsCRM.Web.ViewModels.Promotions;

namespace PromotionsCRM.Web.ViewModels.Clients
{
    public class ClientViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Address { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string? Website { get; set; }

        public string Country{ get; set; } = null!;


    }
}
