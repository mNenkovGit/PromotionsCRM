using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromotionsCRM.Data;
using PromotionsCRM.Data.Models;
using PromotionsCRM.Web.ViewModels.Clients;
using PromotionsCRM.Web.ViewModels.Promotions;

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
            IQueryable<Client> query = _dbContext.Clients;

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(c => c.Name.Contains(name));
            }

            var allClients = query
                .OrderBy(c => c.Name)
                .Select(c => new ClientViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Website = c.Website,
                    Country = c.Country.Name
                })
                .ToArray();

            return View(allClients);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.BadRequest("Client ID is mandatory!");
            }
            var client = _dbContext.Clients
                .Where(c => c.Id == id)
                .Select(c => new ClientDetailsViewModel()
                {
                    Id = c.Id, 
                    Name = c.Name,
                    Email = c.Email,
                    Address = c.Address,
                    Website = c.Website,
                    PhoneNumber = c.PhoneNumber,
                    RegisteredOn = c.RegisteredOn.ToString("dd/MM/yyyy"),
                    Country = c.Country.Name,
                    Promotions = c.Promotions
                        .OrderByDescending(p => p.StartDate)
                        .ThenBy(p => p.Name)
                        .Select(p => new PromotionViewModel()
                        {
                            Id = p.Id,
                            Name = p.Name,
                            StartDate = p.StartDate,
                            EndDate = p.EndDate,
                            PromotionType = p.PromotionType,
                        }).ToArray()
                })
                .SingleOrDefault();

            if (client == null)
            {
                return this.NotFound("Client not found!");
            }
            
            return this.View(client);
        }
    }
}
