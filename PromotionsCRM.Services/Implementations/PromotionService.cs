using Microsoft.EntityFrameworkCore;
using PromotionsCRM.Data;
using PromotionsCRM.Services.Interfaces;
using PromotionsCRM.ViewModels.Promotions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PromotionsCRM.Services.Implementations
{
    public class PromotionService : IPromotionService
    {
        private readonly PromotionsCrmDbContext _dbContext;

        public PromotionService(PromotionsCrmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PromotionViewModel>> GetAllPromotionsAsync(string? name)
        {
            var query = _dbContext.Promotions.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            return await query
                .OrderBy(p => p.Client.Name)
                .ThenByDescending(p => p.StartDate)
                .ThenBy(p => p.Name)
                .Select(p => new PromotionViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    PromotionType = p.PromotionType,
                    ClientName = p.Client.Name,
                    SubmissionCount = p.Submissions.Count
                })
                .ToListAsync();
        }

        public async Task<PromotionDetailsViewModel?> GetPromotionDetailsAsync(int? promotionId)
        {

            return await _dbContext.Promotions
                 .AsNoTracking()
                 .Where(p => p.Id == promotionId)
                 .Select(p => new PromotionDetailsViewModel()
                 {
                     Id = p.Id,
                     Name = p.Name,
                     StartDate = p.StartDate,
                     EndDate = p.EndDate,
                     PromotionType = p.PromotionType,
                     ClientId = p.ClientId,
                     ClientName = p.Client.Name,
                     Countries = p.PromotionsCountries.Select(pc => pc.Country.Name).ToArray(),
                     Products = p.Products.Select(pr => pr.Name).ToArray(),
                     SubmissionCount = p.Submissions.Count,
                     SubmissionStatuses = p.Submissions.Select(s => s.Status).ToArray(),

                 })
                 .FirstOrDefaultAsync();
        }
           
    }
}
