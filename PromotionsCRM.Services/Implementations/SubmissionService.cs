using Microsoft.EntityFrameworkCore;
using PromotionsCRM.Common.Enums;
using PromotionsCRM.Data.Models;
using PromotionsCRM.Data.Repository.Contracts;
using PromotionsCRM.Services.DTOs.Submission.Request;
using PromotionsCRM.Services.DTOs.Submission.Response;
using PromotionsCRM.Services.Interfaces;
using static PromotionsCRM.Common.Validations;

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

        public async Task<PagedResponseDto<SubmissionListItemResponseDto>> GetAllSubmissionsAsync(SubmissionFilterRequestDto filter)
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
            var pageNumber = Math.Max(filter.PageNumber, 1);
            var pageSize = SubmissionsPageSize;

            int totalCount = await query.CountAsync();

            var items = await query
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
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToArrayAsync();

            return new PagedResponseDto<SubmissionListItemResponseDto>()
            {
                Items = items,
                TotalCount = totalCount,
            };
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

        public async Task<SubmissionDetailsResponseDto?> GetSubmissionDetailsAsync(int id)
        {
            var submission = await _submissionRepository.AllAsNoTracking()
                .Where(s => s.Id == id)
                .Select(s => new SubmissionDetailsResponseDto()
                {
                    Id = s.Id,
                    CustomerName = s.Customer.FirstName + " " + s.Customer.LastName,
                    CustomerEmail = s.Customer.Email,
                    CustomerCountry = s.Customer.Country.Name,
                    CustomerPhone = s.Customer.PhoneNumber,
                    PromotionName = s.Promotion.Name,
                    PromotionId = s.PromotionId,
                    ClientName = s.Promotion.Client.Name,
                    PromotionStartDate = s.Promotion.StartDate,
                    PromotionEndDate = s.Promotion.EndDate,
                    ProductName = s.Product.Name,
                    PurchaseDate = s.PurchaseDate,
                    SubmittedOn = s.SubmittedOn,
                    Status = s.Status,
                    ProcessedOn = s.ProcessedOn,
                    ProcessedByEmail = s.ProcessedBy != null ? s.ProcessedBy.Email : null,
                })
                .FirstOrDefaultAsync(); 

            if (submission != null)
            {
                submission.AllowedNextStatuses = GetAllowedNextStatuses(submission.Status);
            }

            return submission;
        }

        private static StatusName[] GetAllowedNextStatuses(StatusName current) => current switch
        {
            StatusName.BeingProcessed => new[] { StatusName.MissingInfo, StatusName.Valid, StatusName.Invalid, StatusName.Cancelled },
            StatusName.MissingInfo => new[] { StatusName.Valid, StatusName.Invalid, StatusName.Cancelled  },
            _ => Array.Empty<StatusName>()
        };

        public async Task<bool> ChangeStatusAsync(int submissionId, StatusName newStatus, string processedById)
        {
           var submission = await _submissionRepository.FindByIdAsync(submissionId);

            if (submission == null)
            {
                return false;
            }

            if (!GetAllowedNextStatuses(submission.Status).Contains(newStatus))
            {
                return false;
            }

            submission.Status = newStatus;
            submission.ProcessedById = processedById;
            submission.ProcessedOn = DateTime.Today;

            await _submissionRepository.SaveChangesAsync();

            return true;
        }
    }
}
