using PromotionsCRM.Common.Enums;

namespace PromotionsCRM.Services.DTOs.Promotion.Response
{
    public class PromotionForDeleteResponseDto 
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public PromotionType PromotionType { get; set; }

        public string ClientName { get; set; } = null!;

        public bool HasSubmissions { get; set; }

        public int ProductsCount { get; set; }
    }
}
