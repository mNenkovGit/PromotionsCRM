using Microsoft.AspNetCore.Mvc;

namespace PromotionsCRM.Web.Controllers
{
    public class ClientsController : Controller
    {
        private static readonly IEnumerable<string> clients = new List<string>()
        {
            "Canon",
            "LG",
            "Epson",
        };

        public IActionResult Index()
        {
            ViewBag.Message = "Corporate clients registration page!";

            ViewData["Clients"] = clients;
            return this.View();
        }

        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return this.BadRequest("Client ID is mandatory!");
            }
            if(id <= 0)
            {
                return this.BadRequest("Client not found!");
            }
            
            return this.Ok($"Client ID: {id}");
        }
    }
}
