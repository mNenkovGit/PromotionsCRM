namespace PromotionsCRM.Services.DTOs.Campaign.Request
{
    public class CampaignRegistrationRequestDto
    {
        public int PromotionId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public int CountryId { get; set; }

        public int ProductId { get; set; }

        public DateTime PurchaseDate { get; set; }

    }
}
