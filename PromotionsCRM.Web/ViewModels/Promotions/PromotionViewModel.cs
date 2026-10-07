
using PromotionsCRM.Web.Data.Enums;


namespace PromotionsCRM.Web.ViewModels.Promotions
{
    public class PromotionViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string PromotionStatus
        {
            get
            {
                if (DateTime.Today < StartDate)
                {
                    return "Upcoming";
                }
                else if (DateTime.Today <= EndDate)
                {
                    return "Ongoing";
                }
                else
                {
                    return "Completed";
                }         
            }
           
        }

        public PromotionType PromotionType { get; set; }

        public string ClientName { get; set; } = null!;

        public int SubmissionCount { get; set; }

    }
}
