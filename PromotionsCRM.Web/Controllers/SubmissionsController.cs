using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PromotionsCRM.Common.Enums;
using PromotionsCRM.Services.DTOs.Submission.Request;
using PromotionsCRM.Services.Interfaces;
using PromotionsCRM.ViewModels.Submissions;

namespace PromotionsCRM.Web.Controllers
{
    public class SubmissionsController : BaseController
    {
        private readonly ISubmissionService _submissionService;
        private readonly ILogger<SubmissionsController> _logger;

        public SubmissionsController(ISubmissionService submissionService, ILogger<SubmissionsController> logger)
        {
            _submissionService = submissionService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(StatusName? status, int? promotionId, string? search)
        {
            var filter = new SubmissionFilterRequestDto()
            {
                Status = status,
                PromotionId = promotionId,
                Search = search
            };

            var responseData = await _submissionService.GetAllSubmissionsAsync(filter);

            var allSubmissionsViewModel = responseData
                .Select(s => new SubmissionListItemViewModel()
                {
                    Id = s.Id,
                    SubmittedOn = s.SubmittedOn,
                    PurchaseDate = s.PurchaseDate,
                    CustomerName = s.CustomerName,
                    CustomerEmail = s.CustomerEmail,
                    PromotionName = s.PromotionName,
                    ProductName = s.ProductName,
                    Status = s.Status,
                })
                .ToArray();

            

            var model = new SubmissionsIndexViewModel()
            {
                Submissions = allSubmissionsViewModel,
                Status = status,
                PromotionId = promotionId,
                Search = search,
                Promotions = await LoadPromotionsAsync()
            };


            return View(model);
        }

        private async Task<IEnumerable<SelectListItem>> LoadPromotionsAsync()
        {
            var promotions = await _submissionService.GetPromotionsForFilterAsync();

            return promotions.Select(p => new SelectListItem()
            {
                Value = p.Id.ToString(),
                Text = p.Name,
            });
        }
    }
}
