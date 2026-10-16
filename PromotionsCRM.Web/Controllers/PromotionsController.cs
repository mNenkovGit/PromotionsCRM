using Microsoft.AspNetCore.Mvc;
using PromotionsCRM.Common.Enums;
using PromotionsCRM.Services.Interfaces;
using PromotionsCRM.ViewModels.Promotions;

namespace PromotionsCRM.Web.Controllers
{
    public class PromotionsController : Controller
    {
        private readonly ILogger<PromotionsController> _logger;

        private readonly IPromotionService _promotionService;

        public PromotionsController(ILogger<PromotionsController> logger, IPromotionService promotionService)
        {
            _logger = logger;
            _promotionService = promotionService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(string? name)
        {

            var allPromotions = await _promotionService.GetAllPromotionsAsync(name);

            return View(allPromotions);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Promotion ID is mandatory!");
            }

            var promotion = await _promotionService.GetPromotionDetailsAsync(id);

            if (promotion == null)
            {
                return NotFound("Promotion not found!");
            }

            return View(promotion);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? clientId)
        {
            if (clientId == null || clientId <= 0)
            {
                return BadRequest("Invalid client ID!");
            }

            var clientName = await _promotionService.GetClientNameAsync(clientId.Value);

            if (clientName == null)
            {
                return NotFound("Client does not exist!");
            }


            var newPromotion = new PromotionCreateViewModel()
            {
                ClientId = clientId.Value,
                ClientName = clientName,
                StartDate = DateTime.Today,
            };

            return View(newPromotion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PromotionCreateViewModel createPromotion)
        {

            string? clientName = await _promotionService.GetClientNameAsync(createPromotion.ClientId);

            if (string.IsNullOrEmpty(clientName))
            {
                ModelState.AddModelError(nameof(createPromotion.ClientId), "Invalid client selected!");
            }

            if (createPromotion.PromotionType.HasValue && !Enum.IsDefined(typeof(PromotionType), createPromotion.PromotionType.Value))
            {
                ModelState.AddModelError(nameof(createPromotion.PromotionType), "Invalid promotion type selected!");
            }
            if (createPromotion.StartDate.HasValue && createPromotion.EndDate.HasValue)
            {
                bool isValidDateRange = createPromotion.StartDate < createPromotion.EndDate;

                if (!isValidDateRange)
                {
                    ModelState.AddModelError(nameof(createPromotion.StartDate), "Start date must be before end date!");
                }

                bool isStartDateInFuture = createPromotion.StartDate >= DateTime.Today;

                if (!isStartDateInFuture)
                {
                    ModelState.AddModelError(nameof(createPromotion.StartDate), "Start date must be today or in the future!");
                }

                bool isEndDateInFuture = createPromotion.EndDate >= DateTime.Today;

                if (!isEndDateInFuture)
                {
                    ModelState.AddModelError(nameof(createPromotion.EndDate), "End date must be today or in the future!");
                }
            }

            if (!string.IsNullOrWhiteSpace(createPromotion.Name) && await _promotionService.PromotionNameExistsAsync(createPromotion.ClientId, createPromotion.Name))
            {
                ModelState.AddModelError(nameof(createPromotion.Name), "This client already has a promotion with this name!");
            }

            if (!ModelState.IsValid)
            {
                createPromotion.ClientName = clientName;
                return View(createPromotion);
            }
            try
            {
                await _promotionService.CreatePromotionAsync(createPromotion);

                TempData["SuccessMessage"] = "Promotion created successfully!";

                return RedirectToAction(nameof(ClientsController.Details), "Clients", new { id = createPromotion.ClientId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the promotion!");

                createPromotion.ClientName = clientName;
                TempData["ErrorMessage"] = "An error occurred while creating the promotion. Please try again later!";

                return View(createPromotion);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest("Invalid Promotion ID!");
            }

            var promotionForEdit = await _promotionService.GetPromotionForEditAsync(id.Value);

            if (promotionForEdit == null)
            {
                return NotFound("Promotion not found!");
            }


            return View(promotionForEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PromotionEditViewModel model)
        {

            if (id <= 0)
            {
                return BadRequest("There was an error while editing the promotion! Please try again!");
            }

            int? clientId = await _promotionService.GetPromotionClientIdAsync(id);

            if (clientId == null)
            {
                return NotFound("Promotion not found!");
            }

            if (!string.IsNullOrWhiteSpace(model.Name) && await _promotionService.PromotionNameExistsAsync(clientId.Value, model.Name, id))
            {
                ModelState.AddModelError(nameof(model.Name), "This client already has a promotion with this name!");
            }

            if (model.PromotionType.HasValue && !Enum.IsDefined(typeof(PromotionType), model.PromotionType.Value))
            {
                ModelState.AddModelError(nameof(model.PromotionType), "Invalid promotion type selected!");
            }

            if (model.StartDate.HasValue && model.EndDate.HasValue)
            {
                bool isValidDateRange = model.StartDate < model.EndDate;

                if (!isValidDateRange)
                {
                    ModelState.AddModelError(nameof(model.StartDate), "Start date must be before end date!");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                bool isEdited = await _promotionService.EditPromotionAsync(id, model);

                if (!isEdited)
                {
                    return NotFound("Promotion not found or could not be edited!");
                }

                TempData["SuccessMessage"] = "Promotion edited successfully!";
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while editing the promotion!");

                TempData["ErrorMessage"] = "Unexpected error occurred while editing the promotion!";


                return View(model);
            }

            return RedirectToAction(nameof(PromotionsController.Details), "Promotions", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Promotion ID!");
            }
            var promotion = await _promotionService.GetPromotionForDeletionAsync(id);

            if (promotion == null)
            {
                return NotFound("Promotion not found!");
            }

            return View(promotion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Promotion ID!");
            }
            bool hasSubmissions = await _promotionService.HasSubmissionsAsync(id);

            if (hasSubmissions)
            {
                _logger.LogWarning("There are already submitted registrations for this promotion! Delete Action is denied!");

                TempData["ErrorMessage"] = "This promotion has submissions and cannot be deleted!";
                return RedirectToAction(nameof(Details), new { id });
            }

            try
            {
                var isDeleted = await _promotionService.DeletePromotionAsync(id);
                if (!isDeleted)
                {
                    return NotFound("Promotion not found or could not be deleted!");
                }

                TempData["SuccessMessage"] = "Promotion deleted successfully!";
                return RedirectToAction(nameof(PromotionsController.Index), "Promotions");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting promotion {PromotionId}", id);
                TempData["ErrorMessage"] = "Unexpected error occurred while deleting promotion! Try again later!";

                return RedirectToAction(nameof(Details), new { id });
            }

        }

    }
}
