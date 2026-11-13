using PromotionsCRM.Services.DTOs.Client.Response;

namespace PromotionsCRM.Services.DTOs.Campaign.Response
{
    public class CampaignRegistrationFormResponseDto
    {
        public int PromotionId { get; set; }

        public string PromotionName { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ICollection<CountryResponseDto> Countries { get; set; } = new List<CountryResponseDto>();

        public ICollection<ProductResponseDto> Products { get; set; } = new List<ProductResponseDto>();
    }
}
