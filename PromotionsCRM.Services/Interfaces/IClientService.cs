using Microsoft.AspNetCore.Mvc.Rendering;
using PromotionsCRM.ViewModels.Clients;
using PromotionsCRM.ViewModels.Promotions;

namespace PromotionsCRM.Services.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientViewModel>> GetAllClientsAsync(string? name);

        Task<ClientDetailsViewModel?> GetClientDetailsAsync(int clientId);

         Task<ClientEditViewModel?> GetClientForEditAsync(int clientId);

        Task<bool> EditClientAsync(int clientId, ClientEditViewModel model);

        Task<IEnumerable<SelectListItem>?> GetCountriesForDropdownAsync();

        Task<bool> CountryExistsAsync(int countryId);
    }
}
