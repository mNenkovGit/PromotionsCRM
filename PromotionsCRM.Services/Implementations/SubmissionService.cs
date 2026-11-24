using Microsoft.EntityFrameworkCore;
using PromotionsCRM.Data.Repository.Contracts;
using PromotionsCRM.Services.DTOs.Submission.Response;
using PromotionsCRM.Services.Interfaces;
using PromotionsCRM.Common.Enums;

namespace PromotionsCRM.Services.Implementations
{
    public class SubmissionService : ISubmissionService
    {
        private readonly ISubmissionRepository _submissionRepository;

        public SubmissionService(ISubmissionRepository submissionRepository)
        {
            _submissionRepository = submissionRepository;
        }

        public async Task<IEnumerable<SubmissionListItemResponseDto>> GetAllSubmissionsAsync()
        {
            return await _submissionRepository.AllAsNoTracking()
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
    }
}
