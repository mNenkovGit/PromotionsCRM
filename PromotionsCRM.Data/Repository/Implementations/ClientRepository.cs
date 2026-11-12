using PromotionsCRM.Data.Models;
using PromotionsCRM.Data.Repository.Common;
using PromotionsCRM.Data.Repository.Contracts;

namespace PromotionsCRM.Data.Repository.Implementations
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(PromotionsCrmDbContext dbContext) : base(dbContext)
        {
        }
    }
}
