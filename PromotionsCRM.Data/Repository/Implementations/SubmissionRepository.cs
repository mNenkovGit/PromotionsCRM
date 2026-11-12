using PromotionsCRM.Data.Models;
using PromotionsCRM.Data.Repository.Common;
using PromotionsCRM.Data.Repository.Contracts;

namespace PromotionsCRM.Data.Repository.Implementations
{
    public class SubmissionRepository : BaseRepository<Submission>, ISubmissionRepository
    {
        public SubmissionRepository(PromotionsCrmDbContext dbContext) : base(dbContext)
        {
        }
    }
}
