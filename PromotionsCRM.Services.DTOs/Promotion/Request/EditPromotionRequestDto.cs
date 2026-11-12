using PromotionsCRM.Common.Enums;

namespace PromotionsCRM.Services.DTOs.Promotion.Request
{
    public class EditPromotionRequestDto
    {
        public string Name { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public PromotionType PromotionType { get; set; }
    }
}
