using PromotionsCRM.Common.Enums;

namespace PromotionsCRM.Services.DTOs.Promotion.Response
{
    public class PromotionDetailsResponseDto : PromotionListItemResponseDto
    {
        public int ClientId { get; set; }

        public ICollection<string> Countries { get; set; } = new List<string>();

        public ICollection<string> Products { get; set; } = new List<string>();

        public ICollection<StatusName> SubmissionStatuses { get; set; } = new List<StatusName>();
    }
}
