using Microsoft.AspNetCore.Mvc.Rendering;
using PromotionsCRM.ViewModels.Clients;
using PromotionsCRM.ViewModels.Promotions;

namespace PromotionsCRM.Services.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientViewModel>> GetAllClientsAsync(string? name);

        Task<ClientDetailsViewModel?> GetClientDetailsAsync(int id, string? name);

         Task<ClientEditViewModel?> GetClientForEditAsync(int id);

        Task<bool> EditClientAsync(int clientId, ClientEditViewModel model);

        Task<IEnumerable<SelectListItem>?> GetCountriesForDropdownAsync();

        Task<bool> CountryExistsAsync(int countryId);
    }
}
