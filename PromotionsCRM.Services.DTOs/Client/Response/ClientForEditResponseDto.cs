namespace PromotionsCRM.Services.DTOs.Client.Response
{
    public class ClientForEditResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Address { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string? Website { get; set; }

        public int CountryId { get; set; }

        public IEnumerable<CountryResponseDto>? Countries { get; set; }

    }
}
