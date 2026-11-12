using Microsoft.EntityFrameworkCore;
using PromotionsCRM.Data.Models;
using PromotionsCRM.Data.Repository.Contracts;
using PromotionsCRM.Services.DTOs.Client.Request;
using PromotionsCRM.Services.DTOs.Client.Response;
using PromotionsCRM.Services.Interfaces;

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

        public async Task<IEnumerable<ClientResponseDto>> GetAllClientsAsync(string? name)
        {
            IQueryable<Client> query = _clientRepository.AllAsNoTracking();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(c => c.Name.Contains(name));
            }

            return await query
                .OrderBy(c => c.Name)
                .Select(c => new ClientResponseDto()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    Website = c.Website,
                    Country = c.Country.Name
                })
                .ToArrayAsync();
        }

        public async Task<ClientDetailsResponseDto?> GetClientDetailsAsync(int id, string? name)
        {
            return await _clientRepository.AllAsNoTracking()
               .Where(c => c.Id == id)
               .Select(c => new ClientDetailsResponseDto()
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
                       .Select(p => new ClientPromotionResponseDto()
                       {
                           Id = p.Id,
                           Name = p.Name,
                           StartDate = p.StartDate,
                           EndDate = p.EndDate,
                           PromotionType = p.PromotionType
                       }).ToArray()
               })
               .SingleOrDefaultAsync();
        }

        public async Task<ClientForEditResponseDto?> GetClientForEditAsync(int clientId)
        {
            return await _clientRepository.AllAsNoTracking()
                .Where(c => c.Id == clientId)
                .Select(c => new ClientForEditResponseDto()
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

        public async Task<bool> EditClientAsync(int clientId, EditClientRequestData model)
        {
            var clientToEdit = await _clientRepository.FindByIdAsync(clientId);

            if (clientToEdit is null)
            {
                return false;
            }

            clientToEdit.Name = model.Name.Trim();
            clientToEdit.Email = model.Email;
            clientToEdit.Address = model.Address;
            clientToEdit.Website = model.Website;
            clientToEdit.PhoneNumber = model.PhoneNumber;
            clientToEdit.CountryId = model.CountryId;

            await _clientRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CountryResponseDto>> GetCountriesForDropdownAsync()
        {
            return await _countryRepository.AllAsNoTracking()
                .OrderBy(c => c.Name)
                .Select(c => new CountryResponseDto()
                {
                    Id = c.Id,
                    Name = c.Name
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
