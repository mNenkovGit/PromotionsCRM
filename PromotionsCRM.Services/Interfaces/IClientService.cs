using PromotionsCRM.Services.DTOs.Client.Request;
using PromotionsCRM.Services.DTOs.Client.Response;

namespace PromotionsCRM.Services.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientResponseDto>> GetAllClientsAsync(string? name);

        Task<ClientDetailsResponseDto?> GetClientDetailsAsync(int id, string? name);

        Task<ClientForEditResponseDto?> GetClientForEditAsync(int id);

        Task<bool> EditClientAsync(int clientId, EditClientRequestData model);

        Task<IEnumerable<CountryResponseDto>> GetCountriesForDropdownAsync();

        Task<bool> CountryExistsAsync(int countryId);

        Task<bool> ClientNameExistsAsync(string name, int excludedClientId);
    }
}
