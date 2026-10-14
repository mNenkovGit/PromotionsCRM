

using PromotionsCRM.ViewModels.Promotions;

namespace PromotionsCRM.Services.Interfaces
{
    public interface IPromotionService
    {
        Task<IEnumerable<PromotionViewModel>> GetAllPromotionsAsync(string? name);

        Task<PromotionDetailsViewModel?> GetPromotionDetailsAsync(int? promotionId);
    }
}
