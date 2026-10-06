using Microsoft.AspNetCore.Mvc;
using PromotionsCRM.Data;

namespace PromotionsCRM.Web.Controllers
{
    public class PromotionsController1 : Controller
    {
        private readonly PromotionsCrmDbContext _dbContext;
        public PromotionsController1(PromotionsCrmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var allPromotions = _dbContext.Promotions.ToList();

            return View();
        }

        public IActionResult Details(int id)
        {
            // Logic to retrieve promotion details by id
            return View();
        }
        public IActionResult Create()
        {
            // Logic to show create promotion form
            return View();
        }
    }
}
