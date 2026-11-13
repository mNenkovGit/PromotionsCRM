using Microsoft.EntityFrameworkCore;
using PromotionsCRM.Data.Repository.Contracts;
using PromotionsCRM.Services.DTOs.Campaign.Response;
using PromotionsCRM.Services.DTOs.Client.Response;
using PromotionsCRM.Services.Interfaces;

namespace PromotionsCRM.Services.Implementations
{
    public class CampaignService : ICampaignService
    {
        private readonly IPromotionRepository _promotionRepository;

        public CampaignService(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        public async Task<CampaignDetailsResponseDto?> GetActiveCampaignDetailsAsync(int promotionId)
        {
            var today = DateTime.Today;
            return await _promotionRepository.AllAsNoTracking()
                .Where(p => p.Id == promotionId && p.StartDate <= today && p.EndDate >= today)
                .Select(p => new CampaignDetailsResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    PromotionType = p.PromotionType,
                    ClientName = p.Client.Name,
                    Countries = p.PromotionsCountries
                            .Select(pc => pc.Country.Name)
                            .ToArray(),
                    Products = p.Products
                            .Select(pr => pr.Name)
                            .ToArray()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ActiveCampaignResponseDto>> GetActiveCampaignsAsync(string? name)
        {
            var today = DateTime.Today;

            var query = _promotionRepository.AllAsNoTracking();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            return await query
                .Where(p => p.StartDate <= today && p.EndDate >= today)
                .Select(p => new ActiveCampaignResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    PromotionType = p.PromotionType,
                    ClientName = p.Client.Name,
                    Countries = p.PromotionsCountries
                            .Select(pc => pc.Country.Name)
                            .ToArray(),
                    Products = p.Products
                            .Select(pr => pr.Name)
                            .ToArray()
                })
                .OrderBy(p => p.EndDate)
                .ToArrayAsync();
        }

        public async Task<CampaignRegistrationFormResponseDto?> GetRegistrationFormDataAsync(int promotionId)
        {
            var today = DateTime.Today;

            return await _promotionRepository.AllAsNoTracking()
                 .Where(p => p.Id == promotionId && p.StartDate <= today && p.EndDate >= today)
                 .Select(p => new CampaignRegistrationFormResponseDto()
                 {
                     PromotionId = p.Id,
                     PromotionName = p.Name,
                     StartDate = p.StartDate,
                     EndDate = p.EndDate,
                     Countries = p.PromotionsCountries
                             .Select(pc => new CountryResponseDto()
                             {
                                 Id = pc.Country.Id,
                                 Name = pc.Country.Name
                             })
                             .ToArray(),
                     Products = p.Products
                             .Select(pr => new ProductResponseDto
                             {
                                 Id = pr.Id,
                                 Name = pr.Name
                             })
                             .ToArray()
                 }).FirstOrDefaultAsync();
        }
    }
}
