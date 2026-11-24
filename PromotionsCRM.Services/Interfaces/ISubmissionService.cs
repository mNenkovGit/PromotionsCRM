using PromotionsCRM.Services.DTOs.Submission.Response;

namespace PromotionsCRM.Services.Interfaces
{
    public interface ISubmissionService
    {
        Task<IEnumerable<SubmissionListItemResponseDto>> GetAllSubmissionsAsync();
    }
}
