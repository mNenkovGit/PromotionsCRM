using PromotionsCRM.Common.Enums;

namespace PromotionsCRM.ViewModels.Submissions
{
    public class SubmissionListItemViewModel
    {
        public int Id { get; set; }

        public DateTime SubmittedOn { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string CustomerName { get; set; } = null!;

        public string CustomerEmail { get; set; } = null!;

        public string PromotionName { get; set; } = null!;

        public string ProductName { get; set; } = null!;

        public StatusName Status { get; set; }
    }
}
