using PromotionsCRM.Common.Enums;

namespace PromotionsCRM.ViewModels.Campaign
{
    public class ActiveCampaignViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public PromotionType PromotionType { get; set; }

        public string DaysLeft
        {
            get
            {
                string daysLeft = (EndDate - DateTime.Today).Days.ToString();
                return daysLeft;
            }
        }

        public string ClientName { get; set; } = null!;

        public ICollection<string> Countries { get; set; } = new List<string>();

        public ICollection<string> Products { get; set; } = new List<string>();
    }
}
