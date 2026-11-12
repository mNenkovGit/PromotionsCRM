using PromotionsCRM.Services.DTOs.Promotion.Response;
using PromotionsCRM.ViewModels.Promotions;

namespace PromotionsCRM.Services.Interfaces
{
    public interface IPromotionService
    {
        Task<IEnumerable<PromotionListItemResponseDto>> GetAllPromotionsAsync(string? name);

        Task<PromotionDetailsResponseDto?> GetPromotionDetailsAsync(int promotionId);

        Task<string?> GetClientNameAsync(int clientId);

        Task CreatePromotionAsync(PromotionCreateViewModel createPromotion);

        Task<PromotionForEditResponseDto?> GetPromotionForEditAsync(int promotionId);

        Task<bool> EditPromotionAsync(int promotionId, PromotionEditViewModel model);

        Task<PromotionForDeleteResponseDto?> GetPromotionForDeletionAsync(int promotionId);

        Task<bool> DeletePromotionAsync(int promotionId);

        Task<bool> HasSubmissionsAsync(int promotionId);

        Task<bool> PromotionNameExistsAsync(int clientId, string name, int? excludedPromotionId = null);

        Task<int?> GetPromotionClientIdAsync(int promotionId);
    }
}
