using PromotionsCRM.Services.DTOs.Submission.Request;
using PromotionsCRM.Services.DTOs.Submission.Response;

namespace PromotionsCRM.Services.Interfaces
{
    public interface ISubmissionService
    {
        Task<IEnumerable<SubmissionListItemResponseDto>> GetAllSubmissionsAsync(SubmissionFilterRequestDto filter);

        Task<IEnumerable<PromotionsResponseDto>> GetPromotionsForFilterAsync();
    }
}
