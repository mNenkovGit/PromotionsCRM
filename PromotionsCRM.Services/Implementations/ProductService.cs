using Microsoft.EntityFrameworkCore;
using PromotionsCRM.Data;
using PromotionsCRM.Data.Models;
using PromotionsCRM.Data.Repository.Contracts;
using PromotionsCRM.Services.Interfaces;
using PromotionsCRM.ViewModels.Products;

namespace PromotionsCRM.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IPromotionRepository _promotionRepository;

        public ProductService(IProductRepository productRepository, IPromotionRepository promotionRepository)
        {
            _productRepository = productRepository;
            _promotionRepository = promotionRepository;
        }

        public async Task CreateProductAsync(ProductCreateViewModel createProduct)
        {
            Product newProduct = new Product()
            {
                Name = createProduct.Name.Trim(),
                PromotionId = createProduct.PromotionId
            };

            await _productRepository.AddAsync(newProduct);
            await _productRepository.SaveChangesAsync();
        }

        public async Task<string?> GetPromotionNameAsync(int promotionId)
        {
            return await _promotionRepository.AllAsNoTracking()
                   .Where(p => p.Id == promotionId)
                   .Select(p => p.Name)
                   .FirstOrDefaultAsync();
        }

        public async Task<bool> ProductNameExistsAsync(int promotionId, string name)
        {
            string productNameWithoutSpaces = name.Replace(" ", "");

            return await _productRepository.AllAsNoTracking().AnyAsync(p => p.Name.Replace(" ", "") == productNameWithoutSpaces && p.PromotionId == promotionId);
        }
    }
}
