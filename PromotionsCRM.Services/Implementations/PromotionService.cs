using Microsoft.EntityFrameworkCore;
using PromotionsCRM.Data;
using PromotionsCRM.Data.Models;
using PromotionsCRM.Services.Interfaces;
using PromotionsCRM.ViewModels.Promotions;

namespace PromotionsCRM.Services.Implementations
{
    public class PromotionService : IPromotionService
    {
        private readonly PromotionsCrmDbContext _dbContext;

        public PromotionService(PromotionsCrmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreatePromotionAsync(PromotionCreateViewModel createPromotion)
        {
            Promotion newPromotion = new Promotion()
            {
                Name = createPromotion.Name.Trim(),
                StartDate = createPromotion.StartDate.Value,
                EndDate = createPromotion.EndDate.Value,
                PromotionType = createPromotion.PromotionType.Value,
                ClientId = createPromotion.ClientId
            };

            _dbContext.Promotions.Add(newPromotion);
            await _dbContext.SaveChangesAsync();


        }

        public async Task<bool> DeletePromotionAsync(int promotionId)
        {
            var promotionToDelete = await _dbContext.Promotions.FindAsync(promotionId);

            if (promotionToDelete == null)
            {
                return false;
            }

            _dbContext.Promotions.Remove(promotionToDelete);

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditPromotionAsync(int promotionId, PromotionEditViewModel model)
        {
            var promotionToEdit = await _dbContext.Promotions.FindAsync(promotionId);

            if (promotionToEdit == null)
            {
                return false;
            }

            promotionToEdit.Name = model.Name.Trim();
            promotionToEdit.StartDate = model.StartDate.Value;
            promotionToEdit.EndDate = model.EndDate.Value;
            promotionToEdit.PromotionType = model.PromotionType.Value;

            await _dbContext.SaveChangesAsync();
            return true;
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

        public async Task<string?> GetClientNameAsync(int clientId)
        {
            return await _dbContext.Clients
                         .Where(c => c.Id == clientId)
                         .Select(c => c.Name)
                         .FirstOrDefaultAsync();
        }

        public async Task<PromotionDetailsViewModel?> GetPromotionDetailsAsync(int promotionId)
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

        public async Task<PromotionDeleteViewModel?> GetPromotionForDeletionAsync(int promotionId)
        {
            return await _dbContext.Promotions
                .AsNoTracking()
                .Where(p => p.Id == promotionId)
                .Select(p => new PromotionDeleteViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    PromotionType = p.PromotionType,
                    ClientName = p.Client.Name,
                    HasSubmissions = p.Submissions.Any(),
                    ProductsCount = p.Products.Count(),
                })
                .FirstOrDefaultAsync();

        }

        public async Task<PromotionEditViewModel?> GetPromotionForEditAsync(int promotionId)
        {
            return await _dbContext.Promotions
                .AsNoTracking()
                .Where(p => p.Id == promotionId)
                .Select(p => new PromotionEditViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    PromotionType = p.PromotionType
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> HasSubmissionsAsync(int promotionId)
        {
            var hasSubmissions = await _dbContext.Submissions.AnyAsync(s => s.PromotionId == promotionId);

            return hasSubmissions;
        }

        public async Task<bool> PromotionNameExistsAsync(int clientId, string name, int? excludedPromotionId = null)
        {
            string promotionNameWithoutSpaces = name.Replace(" ", "");

            return await _dbContext.Promotions.AnyAsync(p => p.Name.Replace(" ", "") == promotionNameWithoutSpaces
                            && p.ClientId == clientId
                            && (excludedPromotionId == null || p.Id != excludedPromotionId));
        }

        public async Task<int?> GetPromotionClientIdAsync(int promotionId)
        {
            return await _dbContext.Promotions
                .Where(p => p.Id == promotionId)
                .Select(p => (int?)p.ClientId)
                .FirstOrDefaultAsync();
        }
    }
}
