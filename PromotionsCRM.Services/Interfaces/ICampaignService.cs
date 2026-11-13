using PromotionsCRM.Services.DTOs.Campaign.Request;
using PromotionsCRM.Services.DTOs.Campaign.Response;
using PromotionsCRM.Services.DTOs.Promotion.Response;

namespace PromotionsCRM.Services.Interfaces
{
    public interface ICampaignService
    {
        Task<IEnumerable<ActiveCampaignResponseDto>> GetActiveCampaignsAsync(string? name);

        Task<CampaignDetailsResponseDto?> GetActiveCampaignDetailsAsync(int promotionId);

        
        Task<CampaignRegistrationFormResponseDto> GetRegistrationFormDataAsync(int promotionId);
    }
}
