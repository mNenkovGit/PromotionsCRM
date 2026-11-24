using PromotionsCRM.Common.Enums;
using PromotionsCRM.Services.DTOs.Submission.Request;
using PromotionsCRM.Services.DTOs.Submission.Response;

namespace PromotionsCRM.Services.Interfaces
{
    public interface ISubmissionService
    {
        Task<PagedResponseDto<SubmissionListItemResponseDto>> GetAllSubmissionsAsync(SubmissionFilterRequestDto filter);

        Task<IEnumerable<PromotionsResponseDto>> GetPromotionsForFilterAsync();

        Task<SubmissionDetailsResponseDto?> GetSubmissionDetailsAsync(int id);

        Task<bool> ChangeStatusAsync(int submissionId, StatusName newStatus, string processedById);
    }
}
