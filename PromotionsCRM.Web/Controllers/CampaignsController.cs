using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PromotionsCRM.Data.Models;
using PromotionsCRM.Services.Interfaces;
using PromotionsCRM.ViewModels.Campaign;
using PromotionsCRM.ViewModels.Campaigns;
using PromotionsCRM.ViewModels.Promotions;

namespace PromotionsCRM.Web.Controllers
{
    [AllowAnonymous]
    public class CampaignsController : Controller
    {

        private readonly ICampaignService _campaignService;

        public CampaignsController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        public async Task<IActionResult> Index(string? name)
        {
            var allCampaignsResponseDto = await _campaignService.GetActiveCampaignsAsync(name);

            var allCampaignsViewModel = allCampaignsResponseDto
                .Select(c => new ActiveCampaignViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    PromotionType = c.PromotionType,
                    ClientName = c.ClientName,
                    Countries = c.Countries,
                    Products = c.Products
                })
                .ToList();

            return View(allCampaignsViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Promotion ID!");
            }

            var campaignDetailsResponseDto = await _campaignService.GetActiveCampaignDetailsAsync(id);
            
            if (campaignDetailsResponseDto == null)
            {
                return NotFound();
            }
            
            var campaignDetailsViewModel = new CampaignDetailsViewModel()
            {
                Id = campaignDetailsResponseDto.Id,
                Name = campaignDetailsResponseDto.Name,
                StartDate = campaignDetailsResponseDto.StartDate,
                EndDate = campaignDetailsResponseDto.EndDate,
                PromotionType = campaignDetailsResponseDto.PromotionType,
                ClientName = campaignDetailsResponseDto.ClientName,
                Countries = campaignDetailsResponseDto.Countries,
                Products = campaignDetailsResponseDto.Products
            };
            
            return View(campaignDetailsViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Register(int promotionId)
        {
            if (promotionId <= 0)
            {
                return BadRequest("Invalid promotion!");
            }

            var campaignDto = await _campaignService.GetRegistrationFormDataAsync(promotionId);

            if (campaignDto == null)
            {
                return NotFound("Promotion does not exist!");
            }

            var campaignViewModel = new CampaignRegistrationViewModel()
            {
                PromotionId = campaignDto.PromotionId,
                PromotionName = campaignDto.PromotionName,
                StartDate = campaignDto.StartDate,
                EndDate = campaignDto.EndDate,
                Countries = campaignDto.Countries
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }),
                Products = campaignDto.Products
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                })

            };

            return View(campaignViewModel);
        }
    }
}
