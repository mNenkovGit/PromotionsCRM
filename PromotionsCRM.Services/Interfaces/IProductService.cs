using PromotionsCRM.Services.DTOs.Product.Request;
using PromotionsCRM.ViewModels.Products;

namespace PromotionsCRM.Services.Interfaces
{
    public interface IProductService
    {
        Task CreateProductAsync(CreateProductRequestDto createProduct);

        Task<string?> GetPromotionNameAsync(int promotionId);

        Task<bool> ProductNameExistsAsync(int promotionId, string name);
    }
}
