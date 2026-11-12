using PromotionsCRM.Common.Enums;

namespace PromotionsCRM.Services.DTOs.Promotion.Request
{
    public class CreatePromotionRequestDto
    {
        public string Name { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public PromotionType PromotionType { get; set; }

        public int ClientId { get; set; }
    }
}
