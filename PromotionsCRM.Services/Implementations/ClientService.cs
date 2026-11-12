using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PromotionsCRM.Data.Models;
using PromotionsCRM.Data.Repository.Contracts;
using PromotionsCRM.Services.Interfaces;
using PromotionsCRM.ViewModels.Clients;
using PromotionsCRM.ViewModels.Promotions;

namespace PromotionsCRM.Services.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICountryRepository _countryRepository;

        public ClientService(IClientRepository clientRepository, ICountryRepository countryRepository)
        {      
            _clientRepository = clientRepository;
            _countryRepository = countryRepository;
        }

        public async Task<IEnumerable<ClientViewModel>> GetAllClientsAsync(string? name)
        {
            IQueryable<Client> query = _clientRepository.AllAsNoTracking();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(c => c.Name.Contains(name));
            }

            return await query
                .OrderBy(c => c.Name)
                .Select(c => new ClientViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Website = c.Website,
                    Country = c.Country.Name
                })
                .ToArrayAsync();
        }

        public async Task<ClientDetailsViewModel?> GetClientDetailsAsync(int id, string? name)
        {
            return await _clientRepository.AllAsNoTracking()
               .Where(c => c.Id == id)
               .Select(c => new ClientDetailsViewModel()
               {
                   Id = c.Id,
                   Name = c.Name,
                   Email = c.Email,
                   Address = c.Address,
                   Website = c.Website,
                   PhoneNumber = c.PhoneNumber,
                   RegisteredOn = c.RegisteredOn,
                   Country = c.Country.Name,
                   Promotions = c.Promotions
                       .Where(p => name == null || p.Name.Contains(name))
                       .OrderByDescending(p => p.StartDate)
                       .ThenBy(p => p.Name)
                       .Select(p => new PromotionViewModel()
                       {
                           Id = p.Id,
                           Name = p.Name,
                           StartDate = p.StartDate,
                           EndDate = p.EndDate,
                           PromotionType = p.PromotionType,
                       }).ToArray()
               })
               .SingleOrDefaultAsync();
        }

        public async Task<ClientEditViewModel?> GetClientForEditAsync(int clientId)
        {
            return await _clientRepository.AllAsNoTracking()
                .Where(c => c.Id == clientId)
                .Select(c => new ClientEditViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    Address = c.Address,
                    Website = c.Website,
                    PhoneNumber = c.PhoneNumber,
                    CountryId = c.CountryId,
                }
                ).FirstOrDefaultAsync();
        }

        public async Task<bool> EditClientAsync(int clientId, ClientEditViewModel model)
        {
            var clientForEdit = await _clientRepository.FindByIdAsync(clientId);

            if (clientForEdit is null)
            {
                return false;
            }

            clientForEdit.Name = model.Name.Trim();
            clientForEdit.Email = model.Email;
            clientForEdit.Address = model.Address;
            clientForEdit.Website = model.Website;
            clientForEdit.PhoneNumber = model.PhoneNumber;
            clientForEdit.CountryId = model.CountryId;

            await _clientRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<SelectListItem>> GetCountriesForDropdownAsync()
        {
            return await _countryRepository.AllAsNoTracking()
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToArrayAsync();
        }

        public async Task<bool> CountryExistsAsync(int countryId)
        {
            return await _countryRepository.AllAsNoTracking()
                .AnyAsync(c => c.Id == countryId);
        }

        public async Task<bool> ClientNameExistsAsync(string name, int excludedClientId)
        {
            string clientNameWithoutSpaces = name.Replace(" ", "");

            return await _clientRepository.AllAsNoTracking()
                .AnyAsync(c => c.Name.Replace(" ", "") == clientNameWithoutSpaces && c.Id != excludedClientId);
        }
    }
}
