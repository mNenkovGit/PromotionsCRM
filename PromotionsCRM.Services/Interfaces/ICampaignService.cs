using PromotionsCRM.Services.DTOs.Campaign.Response;

namespace PromotionsCRM.Services.Interfaces
{
    public interface ICampaignService
    {
        Task<IEnumerable<ActiveCampaignResponseDto>> GetActiveCampaignsAsync(string? name);
    }
}
