using Microsoft.EntityFrameworkCore;
using PromotionsCRM.Common.Enums;
using PromotionsCRM.Data.Repository.Contracts;
using PromotionsCRM.Services.DTOs.Submission.Request;
using PromotionsCRM.Services.DTOs.Submission.Response;
using PromotionsCRM.Services.Interfaces;

namespace PromotionsCRM.Services.Implementations
{
    public class SubmissionService : ISubmissionService
    {
        private readonly ISubmissionRepository _submissionRepository;

        private readonly IPromotionRepository _promotionRepository;

        public SubmissionService(ISubmissionRepository submissionRepository, IPromotionRepository promotionRepository   )
        {
            _submissionRepository = submissionRepository;
            _promotionRepository = promotionRepository;
        }

        public async Task<IEnumerable<SubmissionListItemResponseDto>> GetAllSubmissionsAsync(SubmissionFilterRequestDto filter)
        {
            var query = _submissionRepository.AllAsNoTracking();

            if (filter.Status.HasValue)
            {
                query = query.Where(s => s.Status == filter.Status.Value);
            }

            if (filter.PromotionId.HasValue)
            {
                query = query.Where(s => s.PromotionId == filter.PromotionId);
            }

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                string search = filter.Search.Trim();

                query = query.Where(s => s.Customer.Email.Contains(search)
                                      || s.Customer.FirstName.Contains(search)
                                      || s.Customer.LastName.Contains(search));
            }

            return await query
                .Select(s => new SubmissionListItemResponseDto()
                {
                    Id = s.Id,
                    SubmittedOn = s.SubmittedOn,
                    PurchaseDate = s.PurchaseDate,
                    CustomerName = s.Customer.FirstName + " " + s.Customer.LastName,
                    CustomerEmail = s.Customer.Email,
                    PromotionName = s.Promotion.Name,
                    ProductName = s.Product.Name,
                    Status = s.Status,
                })
                .OrderByDescending(s => s.Status == StatusName.BeingProcessed || s.Status == StatusName.MissingInfo)
                .ThenBy(s => s.SubmittedOn)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<PromotionsResponseDto>> GetPromotionsForFilterAsync()
        {
            return await _promotionRepository.AllAsNoTracking()
                .OrderBy(p => p.Name)
                .Select(p =>  new PromotionsResponseDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                })
                .ToArrayAsync();

        }
    }
}
