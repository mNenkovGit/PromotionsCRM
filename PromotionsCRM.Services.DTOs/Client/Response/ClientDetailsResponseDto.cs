namespace PromotionsCRM.Services.DTOs.Client.Response
{
    public class ClientDetailsResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Address { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string? Website { get; set; }

        public string Country { get; set; } = null!;

        public DateTime RegisteredOn { get; set; }

        public ICollection<ClientPromotionResponseDto> Promotions { get; set; } = new List<ClientPromotionResponseDto>();
    }
}
