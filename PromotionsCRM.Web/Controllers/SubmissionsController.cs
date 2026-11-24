using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index()
        {
            var responseData = await _submissionService.GetAllSubmissionsAsync();

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

            return View(allSubmissionsViewModel);
        }
    }
}
