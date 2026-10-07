using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PromotionsCRM.Data;
using PromotionsCRM.Data.Models;
using PromotionsCRM.Web.Data.Enums;
using PromotionsCRM.Web.ViewModels.Clients;
using PromotionsCRM.Web.ViewModels.Promotions;

namespace PromotionsCRM.Web.Controllers
{
    public class PromotionsController : Controller
    {
        private readonly PromotionsCrmDbContext _dbContext;
        private readonly ILogger<PromotionsController> _logger;

        public PromotionsController(ILogger<PromotionsController> logger, PromotionsCrmDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult Index(string? name)
        {
            var query = _dbContext.Promotions.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            var allPromotions = query
                .OrderBy(p => p.Client.Name)
                .ThenByDescending(p => p.StartDate)
                .ThenBy(p => p.Name)
                .Select(p => new PromotionViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    PromotionType = p.PromotionType,
                    ClientName = p.Client.Name,
                    SubmissionCount = p.Submissions.Count
                })
                .ToList();

            return View(allPromotions);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest("Promotion ID is mandatory!");
            }

            var promotion = _dbContext.Promotions
                .Where(p => p.Id == id)
                .Select(p => new PromotionDetailsViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    PromotionType = p.PromotionType,
                    ClientId = p.ClientId,
                    ClientName = p.Client.Name,
                    Countries = p.PromotionsCountries.Select(pc => pc.Country.Name).ToArray(),
                    Products = p.Products.Select(pr => pr.Name).ToArray(),
                    SubmissionCount = p.Submissions.Count,
                    SubmissionStatuses = p.Submissions.Select(s => s.Status).ToList(),
                    
                })
                .FirstOrDefault();  

            if(promotion == null)
            {
                return NotFound($"Promotion with ID {id} not found!");
            }

            return View(promotion);
        }

        [HttpGet]
        public IActionResult Create(int? clientId)
        {
            var newPromotion = new PromotionCreateViewModel()
            {
                Clients = LoadClients(),
                ClientId = clientId ?? 0,
                StartDate = DateTime.Today
                
            };
            
            return View(newPromotion);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]

        public IActionResult Create(PromotionCreateViewModel createPromotion)
        {

            bool isClientValid = _dbContext.Clients.Any(c => c.Id == createPromotion.ClientId);

            if (!isClientValid)
            {
                ModelState.AddModelError(nameof(createPromotion.ClientId), "Invalid client selected");
            }

            if (createPromotion.PromotionType.HasValue && !Enum.IsDefined(typeof(PromotionType), createPromotion.PromotionType.Value))
            {
                ModelState.AddModelError(nameof(createPromotion.PromotionType), "Invalid promotion type selected");
            }
            if (createPromotion.StartDate.HasValue && createPromotion.EndDate.HasValue)
            {
                bool isValidDateRange = createPromotion.StartDate < createPromotion.EndDate;

                if (!isValidDateRange)
                {
                    ModelState.AddModelError(nameof(createPromotion.StartDate), "Start date must be before end date");
                }

                bool isStartDateInFuture = createPromotion.StartDate >= DateTime.Today;

                if (!isStartDateInFuture)
                {
                    ModelState.AddModelError(nameof(createPromotion.StartDate), "Start date must be today or in the future");
                }

                bool isEndDateInFuture = createPromotion.EndDate >= DateTime.Today;

                if (!isEndDateInFuture)
                {
                    ModelState.AddModelError(nameof(createPromotion.EndDate), "End date must be today or in the future");
                }
            }

            if (!ModelState.IsValid)
            {
                createPromotion.Clients = LoadClients();
                return View(createPromotion);
            }

            try
            {
                Promotion newPromotion = new Promotion()
                {
                    Name = createPromotion.Name,
                    StartDate = createPromotion.StartDate.Value,
                    EndDate = createPromotion.EndDate.Value,
                    PromotionType = createPromotion.PromotionType.Value,
                    ClientId = createPromotion.ClientId
                };

                _dbContext.Promotions.Add(newPromotion);
                _dbContext.SaveChanges();

                TempData["SuccessMessage"] = "Promotion created successfully!";

                return RedirectToAction(nameof(ClientsController.Details), "Clients", new { id = createPromotion.ClientId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the promotion");

                createPromotion.Clients = LoadClients();

                TempData["ErrorMessage"] = "An error occurred while creating the promotion. Please try again later.";

                return View(createPromotion);
            }

        }


        [HttpGet]

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest("There is an error with your request! Please try again");
            }

            var promotionForEdit = _dbContext.Promotions
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new PromotionEditViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    PromotionType = p.PromotionType, 
                })
                .FirstOrDefault();

            if (promotionForEdit == null)
            {
                return NotFound("Promotion not found!");
            }

            return View(promotionForEdit);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]

        public IActionResult Edit(int? id, PromotionEditViewModel promotionToEdit)
        {

            if (id == null)
            {
                return BadRequest("There was an error while editing the promtoion! Please try again!");
            }

            var promotion = _dbContext.Promotions.Find(id);
 
            if (promotionToEdit.PromotionType.HasValue && !Enum.IsDefined(typeof(PromotionType), promotionToEdit.PromotionType.Value))
            {
                ModelState.AddModelError(nameof(promotionToEdit.PromotionType), "Invalid promotion type selected");
            }

            if (promotionToEdit.StartDate.HasValue && promotionToEdit.EndDate.HasValue)
            {
                bool isValidDateRange = promotionToEdit.StartDate < promotionToEdit.EndDate;

                if (!isValidDateRange)
                {
                    ModelState.AddModelError(nameof(promotionToEdit.StartDate), "Start date must be before end date");
                }
            }
         
            if (!ModelState.IsValid)
            {
                return View(promotionToEdit);
            }

            if (promotion == null)
            {
                return NotFound("Promotion Not found! Please try again!");
            }

            try
            {
                promotion.Name = promotionToEdit.Name;
                promotion.StartDate = promotionToEdit.StartDate.Value;
                promotion.EndDate = promotionToEdit.EndDate.Value;
                promotion.PromotionType = promotionToEdit.PromotionType.Value;

                _dbContext.SaveChanges();
                TempData["SuccessMessage"]    = "Promotion Edited Successfully!";
            }

            catch (Exception ex)
            {
                _logger.LogError(ex,"Error occured while editing the promtoion! Try again later!");

                TempData["ErrorMessage"] = "Unexpected error occured while editing the promotion!";


                return View(promotionToEdit);
            }



            return RedirectToAction(nameof(PromotionsController.Details), "Promotions", new { id });
        }

        [HttpGet]

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest("Invalid Promotion Id!");
            }

            var promotion = _dbContext.Promotions
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(p => new PromotionDeleteViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    PromotionType = p.PromotionType,
                    ClientName = p.Client.Name,
                    HasSubmissions = p.Submissions.Any(),
                    ProductsCount = p.Products.Count(),
                })
                .FirstOrDefault();

            if (promotion == null)
            {
                return NotFound("Promotion does not exist!");
            }

            return View(promotion);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName("Delete")]

        public IActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return BadRequest("Invalid Promotion Id!");
            }

            var promotion = _dbContext.Promotions.Find(id);

            if (promotion == null)
            {
                return NotFound("Promotion does not exist! Try again!");
            }
            bool hasSubmissions = _dbContext.Submissions.Any(s => s.Promotion.Id == id);

            if (hasSubmissions)
            {
                _logger.LogError("There are already submited registrations for this promotion! Delete Action is denied!");

                TempData["ErrorMessage"] = "This promotion has submissions and cannot be deleted.";
                return RedirectToAction(nameof(Details), new { id });
            }

            try
            {
                _dbContext.Remove(promotion);
                _dbContext.SaveChanges();

                TempData["SuccessMessage"] = "Promotion deleted successfully!";
                return RedirectToAction(nameof(PromotionsController.Index), "Promotions");
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex, "Error occured while deleting a valid promotional data! Check the logs!");
                TempData["ErrorMessage"] = "Unexpected error occured while deleting promtoion! Try again later!";
                
                return RedirectToAction(nameof(Details), new { id });
            }

        }

        private IEnumerable<SelectListItem> LoadClients()
        {
            var allClients = _dbContext.Clients
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem()
                {
                   Value = c.Id.ToString(),
                   Text = c.Name,
                })
                .ToList();
            return allClients;
        }
    }
}
