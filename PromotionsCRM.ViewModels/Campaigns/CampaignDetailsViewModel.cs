using PromotionsCRM.Common.Enums;
using PromotionsCRM.ViewModels.Campaign;

namespace PromotionsCRM.ViewModels.Promotions
{
    public class CampaignDetailsViewModel : ActiveCampaignViewModel
    {
        public int ClientId { get; set; }

        public ICollection<string> Countries { get; set; } = new List<string>();

        public ICollection<string> Products { get; set; } = new List<string>();

        public ICollection<StatusName> SubmissionStatuses { get; set; } = new List<StatusName>();

        public Dictionary<StatusName, int> SubmissionsByStatus => SubmissionStatuses
                    .GroupBy(s => s)
                    .ToDictionary(g => g.Key, g => g.Count());
    }
}
