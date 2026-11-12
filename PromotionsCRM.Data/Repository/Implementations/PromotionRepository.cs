using PromotionsCRM.Data.Models;
using PromotionsCRM.Data.Repository.Common;
using PromotionsCRM.Data.Repository.Contracts;

namespace PromotionsCRM.Data.Repository.Implementations
{
    public class PromotionRepository : BaseRepository<Promotion>, IPromotionRepository
    {
        public PromotionRepository(PromotionsCrmDbContext dbContext) : base(dbContext)
        {
        }
    }
}
