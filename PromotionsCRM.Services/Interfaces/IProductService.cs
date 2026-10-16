using PromotionsCRM.ViewModels.Products;

namespace PromotionsCRM.Services.Interfaces
{
    public interface IProductService
    {
        Task CreateProductAsync(ProductCreateViewModel createProduct);

        Task<string?> GetPromotionNameAsync(int promotionId);
    }
}
