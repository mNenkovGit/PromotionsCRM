using Microsoft.EntityFrameworkCore;
using PromotionsCRM.Data;
using PromotionsCRM.Data.Models;
using PromotionsCRM.Services.Interfaces;
using PromotionsCRM.ViewModels.Products;

namespace PromotionsCRM.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly PromotionsCrmDbContext _dbContext;
        public ProductService(PromotionsCrmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateProductAsync(ProductCreateViewModel createProduct)
        {
            Product newProduct = new Product()
            {
                Name = createProduct.Name.Trim(),
                PromotionId = createProduct.PromotionId
            };

            await _dbContext.Products.AddAsync(newProduct);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string?> GetPromotionNameAsync(int promotionId)
        {
            var newPromotion = new ProductCreateViewModel()
            {
                PromotionName = await _dbContext.Promotions
                   .Where(c => c.Id == promotionId)
                   .Select(c => c.Name)
                   .FirstOrDefaultAsync(),
                PromotionId = promotionId,
            };

            return newPromotion.PromotionName;
        }

        public async Task<bool> ProductNameExistsAsync(int promotionId, string name)
        {
            string productNameWhtoutSpaces = name.Replace(" ", "");

            return await _dbContext.Products.AnyAsync(p => p.Name.Replace(" ", "") == productNameWhtoutSpaces && p.PromotionId == promotionId);
        }
    }
}
