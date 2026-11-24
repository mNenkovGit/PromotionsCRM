using PromotionsCRM.Common.Enums;

namespace PromotionsCRM.Services.DTOs.Submission.Response
{
    public class SubmissionDetailsResponseDto
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = null!;

        public string CustomerEmail { get; set; } = null!;

        public string CustomerPhone { get; set; } = null!;

        public string CustomerCountry { get; set; } = null!;

        public string PromotionName { get; set; } = null!;

        public int PromotionId { get; set; }

        public string ClientName { get; set; } = null!;

        public DateTime PromotionStartDate { get; set; }

        public DateTime PromotionEndDate { get; set; }

        public string ProductName { get; set; } = null!;

        public DateTime PurchaseDate { get; set; }

        public DateTime SubmittedOn { get; set; }

        public StatusName Status { get; set; }

        public DateTime? ProcessedOn { get; set; }
    }
}
