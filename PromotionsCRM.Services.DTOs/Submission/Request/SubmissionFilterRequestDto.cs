using PromotionsCRM.Common.Enums;

namespace PromotionsCRM.Services.DTOs.Submission.Request
{
    public class SubmissionFilterRequestDto
    {
        public StatusName? Status { get; set; }

        public int? PromotionId { get; set; }

        public string? Search { get; set; }
    }
}
