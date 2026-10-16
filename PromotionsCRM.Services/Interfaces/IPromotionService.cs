using PromotionsCRM.ViewModels.Promotions;

namespace PromotionsCRM.Services.Interfaces
{
    public interface IPromotionService
    {
        Task<IEnumerable<PromotionViewModel>> GetAllPromotionsAsync(string? name);

        Task<PromotionDetailsViewModel?> GetPromotionDetailsAsync(int? promotionId);

        Task<string?> GetClientNameAsync(int clientId);

        Task CreatePromotionAsync(PromotionCreateViewModel createPromotion);

        Task<PromotionEditViewModel?> GetPromotionForEditAsync(int promotionId);

        Task<bool> EditPromotionAsync(int promotionId, PromotionEditViewModel model);

        Task<PromotionDeleteViewModel?> GetPromotionForDeletionAsync(int promotionId);

        Task<bool> DeletePromotionAsync(int promotionId);

        Task<bool> HasSubmissionsAsync(int promotionId);
    }
}
