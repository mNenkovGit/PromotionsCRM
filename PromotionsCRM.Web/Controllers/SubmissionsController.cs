using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PromotionsCRM.Common.Enums;
using PromotionsCRM.Services.DTOs.Submission.Request;
using PromotionsCRM.Services.Interfaces;
using PromotionsCRM.ViewModels.Submissions;
using static PromotionsCRM.Common.Validations;


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

        public async Task<IActionResult> Index(StatusName? status, int? promotionId, string? search, int page = 1)
        {
            var filter = new SubmissionFilterRequestDto()
            {
                Status = status,
                PromotionId = promotionId,
                Search = search,
                PageNumber = page
            };

            var responseData = await _submissionService.GetAllSubmissionsAsync(filter);


            var allSubmissionsViewModel = responseData.Items
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

            int totalPages = (int)Math.Ceiling(responseData.TotalCount / (double)SubmissionsPageSize);

            var model = new SubmissionsIndexViewModel()
            {
                Submissions = allSubmissionsViewModel,
                Status = status,
                PromotionId = promotionId,
                Search = search,
                Promotions = await LoadPromotionsAsync(),
                CurrentPage = Math.Max(page, 1),
                TotalPages = totalPages
            };


            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id  < 0)
            {
                return BadRequest("Submission Id is mandatory!");
            }

            var submissionDetailsDto = await _submissionService.GetSubmissionDetailsAsync(id);

            if (submissionDetailsDto == null)
            {
                return NotFound("Submission not found!");
            }

            var submisionDetailsViewModel = new SubmissionDetailsViewModel()
            {
                Id = submissionDetailsDto.Id,
                CustomerName = submissionDetailsDto.CustomerName,
                CustomerEmail = submissionDetailsDto.CustomerEmail,
                CustomerPhone = submissionDetailsDto.CustomerPhone,
                CustomerCountry = submissionDetailsDto.CustomerCountry,
                PromotionName = submissionDetailsDto.PromotionName,
                PromotionId = submissionDetailsDto.PromotionId,
                ClientName = submissionDetailsDto.ClientName,
                PromotionStartDate = submissionDetailsDto.PromotionStartDate,
                PromotionEndDate = submissionDetailsDto.PromotionEndDate,
                ProductName = submissionDetailsDto.ProductName,
                PurchaseDate = submissionDetailsDto.PurchaseDate,
                SubmittedOn = submissionDetailsDto.SubmittedOn,
                Status = submissionDetailsDto.Status,
                ProcessedOn = submissionDetailsDto.ProcessedOn,
                ProcessedByEmail = submissionDetailsDto.ProcessedByEmail
            };

            return View(submisionDetailsViewModel);
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
