namespace PromotionsCRM.Services.DTOs.Product.Request
{
    public class CreateProductRequestDto
    {
        public string Name { get; set; } = null!;

        public int PromotionId { get; set; }
    }
}
