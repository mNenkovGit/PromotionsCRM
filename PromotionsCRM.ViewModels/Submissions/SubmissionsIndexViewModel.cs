using Microsoft.AspNetCore.Mvc.Rendering;
using PromotionsCRM.Common.Enums;

namespace PromotionsCRM.ViewModels.Submissions
{
    public class SubmissionsIndexViewModel
    {
        public IEnumerable<SubmissionListItemViewModel> Submissions { get; set; } = new List<SubmissionListItemViewModel>();

        public StatusName? Status { get; set; }

        public int? PromotionId { get; set; }

        public string? Search { get; set; }

        public IEnumerable<SelectListItem> Promotions { get; set; } = new List<SelectListItem>();
    }
}
