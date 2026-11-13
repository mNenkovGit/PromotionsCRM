using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PromotionsCRM.Data.Models;
using PromotionsCRM.Services.DTOs.Campaign.Request;
using PromotionsCRM.Services.DTOs.Campaign.Response;
using PromotionsCRM.Services.Implementations;
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

        private readonly ILogger<CampaignsController> _logger;

        public CampaignsController(ICampaignService campaignService, ILogger<CampaignsController> logger)
        {
            _campaignService = campaignService;
            _logger = logger;
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

            var formData = await _campaignService.GetRegistrationFormDataAsync(promotionId);

            if (formData == null)
            {
                return NotFound("Promotion does not exist!");
            }

            var model = new CampaignRegistrationViewModel()
            {
                PromotionId = formData.PromotionId,
            };

            FillFormData(model, formData);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CampaignRegistrationViewModel model)
        {
            var formData = await _campaignService.GetRegistrationFormDataAsync(model.PromotionId);

            if (formData == null)
            {
                return NotFound("Invalid Campaign!");
            }

            bool isProductValid = formData.Products.Any(p => p.Id == model.ProductId);

            if (!isProductValid)
            {
                ModelState.AddModelError(nameof(model.ProductId), "Please select a valid product!");
            }

            bool isCountryValid = formData.Countries.Any(c => c.Id == model.CountryId);

            if (!isCountryValid)
            {
                ModelState.AddModelError(nameof(model.CountryId), "Please select a valid country!");
            }

            if (model.PurchaseDate < formData.StartDate || model.PurchaseDate > formData.EndDate || model.PurchaseDate > DateTime.Today)
            {
                ModelState.AddModelError(nameof(model.PurchaseDate), "Purchase date must be within the promotion period and cannot be in the future");
            }

            if (!string.IsNullOrWhiteSpace(model.Email) && isProductValid && await _campaignService.SubmissionExistsAsync(model.Email, model.PromotionId, model.ProductId))
            {
                ModelState.AddModelError(nameof(model.Email), "You have already registered this product for this promotion!");
            }

            if (!ModelState.IsValid)
            {
                FillFormData(model, formData);
                return View(model);
            }
            try
            {
                var campaignDto = new CampaignRegistrationRequestDto()
                {
                    PromotionId = model.PromotionId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    CountryId = model.CountryId,
                    ProductId = model.ProductId,
                    PurchaseDate = model.PurchaseDate!.Value
                };

                await _campaignService.RegisterAsync(campaignDto);

                TempData["SuccessMessage"] = "Your registration is submitted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                FillFormData(model, formData);

                TempData["ErrorMessage"] = "An error occurred while submitting your registration. Please try again later!";

                _logger.LogError(ex, "An error occurred while submitting your registration!");

                return View(model);
            }
        }

        private static void FillFormData(CampaignRegistrationViewModel model, CampaignRegistrationFormResponseDto formData)
        {
            model.PromotionName = formData.PromotionName;
            model.StartDate = formData.StartDate;
            model.EndDate = formData.EndDate;

            model.Countries = formData.Countries
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                });

            model.Products = formData.Products
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                });
        }
    }
}
