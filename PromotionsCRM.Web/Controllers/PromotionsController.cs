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
                    ClientName = p.Client.Name
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
