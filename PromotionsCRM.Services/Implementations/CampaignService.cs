using Microsoft.EntityFrameworkCore;
using PromotionsCRM.Common.Enums;
using PromotionsCRM.Data.Models;
using PromotionsCRM.Data.Repository.Contracts;
using PromotionsCRM.Services.DTOs.Campaign.Request;
using PromotionsCRM.Services.DTOs.Campaign.Response;
using PromotionsCRM.Services.DTOs.Client.Response;
using PromotionsCRM.Services.Interfaces;

namespace PromotionsCRM.Services.Implementations
{
    public class CampaignService : ICampaignService
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ISubmissionRepository _submissionRepository;

        public CampaignService(IPromotionRepository promotionRepository, ISubmissionRepository submissionRepository, ICustomerRepository customerRepository)
        {
            _promotionRepository = promotionRepository;
            _customerRepository = customerRepository;
            _submissionRepository = submissionRepository;
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

        public async Task RegisterAsync(CampaignRegistrationRequestDto registration)
        {
            var trimmedEmail = registration.Email.Trim();

            var customer = await _customerRepository.All()
                .FirstOrDefaultAsync(c => c.Email == trimmedEmail);

            if (customer == null)
            {
                customer = new Customer()
                {
                    FirstName = registration.FirstName,
                    LastName = registration.LastName,
                    Email = registration.Email,
                    PhoneNumber = registration.PhoneNumber.Trim(),
                    CountryId = registration.CountryId,
                    RegisteredOn = DateTime.Today
                };

                await _customerRepository.AddAsync(customer);

                var submission = new Submission()
                {
                    Customer = customer,
                    PromotionId = registration.PromotionId,
                    ProductId = registration.ProductId,
                    PurchaseDate = registration.PurchaseDate,
                    Status = StatusName.BeingProcessed,
                    SubmittedOn = DateTime.Today
                };

                await _submissionRepository.AddAsync(submission);
                await _submissionRepository.SaveChangesAsync();
            }
        }


        public async Task<bool> SubmissionExistsAsync(string email, int promotionId, int productId)
        {
            return await _submissionRepository.AllAsNoTracking()
                .AnyAsync(s => s.Customer.Email == email.Trim()
                && s.PromotionId == promotionId
                && s.ProductId == productId);
        }

    }
}
