using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PromotionsCRM.Services.Interfaces;
using PromotionsCRM.ViewModels.Campaign;
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
    }
}
