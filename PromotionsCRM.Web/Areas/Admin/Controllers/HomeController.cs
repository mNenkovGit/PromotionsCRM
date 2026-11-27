using Microsoft.AspNetCore.Mvc;

namespace PromotionsCRM.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
