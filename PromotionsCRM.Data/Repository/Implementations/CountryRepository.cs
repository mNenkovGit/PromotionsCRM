using PromotionsCRM.Data.Models;
using PromotionsCRM.Data.Repository.Common;
using PromotionsCRM.Data.Repository.Contracts;

namespace PromotionsCRM.Data.Repository.Implementations
{
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(PromotionsCrmDbContext dbContext) : base(dbContext)
        {
        }
    }
}
