using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PromotionsCRM.Services.Interfaces;
using PromotionsCRM.ViewModels.Clients;

namespace PromotionsCRM.Web.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;

        private readonly ILogger<ClientsController> _logger;

        public ClientsController(IClientService clientService, ILogger<ClientsController> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> Index(string? name)
        {
            var allClients = await _clientService.GetAllClientsAsync(name);

            return View(allClients);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, string? name)
        {
            if (id == null || id <= 0)
            {
                return BadRequest("Client ID is mandatory!");
            }

            var client = await _clientService.GetClientDetailsAsync(id.Value, name);

            if (client == null)
            {
                return NotFound("Client not found!");
            }

            return View(client);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest("Client ID is mandatory!");
            }
            var clientToEdit = await _clientService.GetClientForEditAsync(id.Value);

            if (clientToEdit == null)
            {
                return NotFound("Client not found!");
            }

            clientToEdit.Countries = await _clientService.GetCountriesForDropdownAsync();

            return View(clientToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClientEditViewModel model)
        {
            if (id <= 0)
            {
                return BadRequest("Client Id is mandatory!");
            }

            if (!string.IsNullOrWhiteSpace(model.Name) && await _clientService.ClientNameExistsAsync(model.Name, id))
            {
                ModelState.AddModelError(nameof(model.Name), "A client with this name already exists!");
            }

            if (!await _clientService.CountryExistsAsync(model.CountryId))
            {
                ModelState.AddModelError(nameof(model.CountryId), "Country not found!");
            }

            if (!ModelState.IsValid)
            {
                model.Countries = await _clientService.GetCountriesForDropdownAsync();

                return View(model);
            }

            try
            {
                var result = await _clientService.EditClientAsync(id, model);

                if (!result)
                {
                    return NotFound("Client not found!");
                }
                TempData["SuccessMessage"] = "Client edited successfully!";
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while editing the Client! Try again later!");

                TempData["ErrorMessage"] = "Unexpected error occurred while editing the Client!";

                model.Countries = await _clientService.GetCountriesForDropdownAsync();

                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
