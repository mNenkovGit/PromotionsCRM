
namespace PromotionsCRM.Services.DTOs.Client.Request
{
    public class EditClientRequestData
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Address { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string? Website { get; set; }

        public int CountryId { get; set; }
    }
}
