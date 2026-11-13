using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PromotionsCRM.Services.Interfaces;
using PromotionsCRM.ViewModels.Campaign;

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
    }
}
