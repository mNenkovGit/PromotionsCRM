using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromotionsCRM.Data;
using PromotionsCRM.Data.Models;

namespace PromotionsCRM.Web.Controllers
{
    public class ClientsController : Controller
    {
        private readonly PromotionsCrmDbContext _dbContext;
        public ClientsController(PromotionsCrmDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult Index(string? name)
        {
            IQueryable<Client> query = _dbContext.Clients
                .Include(c => c.Country);

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query
                    .Where(c => c.Name.ToLower().Contains(name.ToLower()));
            }

            var allClients = query.OrderBy(c => c.Name).ToList();

            return View(allClients);
        }

        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return this.BadRequest("Client not found!");
            }
            var client = _dbContext.Clients
                .Include(c => c.Country)
                .Include(c => c.Promotions)
                .SingleOrDefault(c => c.Id == id);

            if (client == null)
            {
                return this.NotFound("Client not found!");
            }
            
            return this.View(client);
        }
    }
}
