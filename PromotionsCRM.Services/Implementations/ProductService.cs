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
            return await _dbContext.Promotions
                   .Where(p => p.Id == promotionId)
                   .Select(p => p.Name)
                   .FirstOrDefaultAsync();
        }

        public async Task<bool> ProductNameExistsAsync(int promotionId, string name)
        {
            string productNameWithoutSpaces = name.Replace(" ", "");

            return await _dbContext.Products.AnyAsync(p => p.Name.Replace(" ", "") == productNameWithoutSpaces && p.PromotionId == promotionId);
        }
    }
}
