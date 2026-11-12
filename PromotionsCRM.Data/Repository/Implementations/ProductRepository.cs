using PromotionsCRM.Data.Models;
using PromotionsCRM.Data.Repository.Common;
using PromotionsCRM.Data.Repository.Contracts;

namespace PromotionsCRM.Data.Repository.Implementations
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(PromotionsCrmDbContext dbContext) : base(dbContext)
        {
        }
    }
}
