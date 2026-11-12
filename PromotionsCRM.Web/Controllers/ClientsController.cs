using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PromotionsCRM.Services.DTOs.Client.Request;
using PromotionsCRM.Services.Interfaces;
using PromotionsCRM.ViewModels.Clients;
using PromotionsCRM.ViewModels.Promotions;

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
            var allClientsResponseDto = await _clientService.GetAllClientsAsync(name);

            var allClientsViewModel = allClientsResponseDto
                .Select(c => new ClientViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    Address = c.Address,
                    PhoneNumber = c.PhoneNumber,
                    Website = c.Website,
                    Country = c.Country
                })
                .ToList();

            return View(allClientsViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, string? name)
        {
            if (id == null || id <= 0)
            {
                return BadRequest("Client ID is mandatory!");
            }

            var clientResponseDto = await _clientService.GetClientDetailsAsync(id.Value, name);

            if (clientResponseDto == null)
            {
                return NotFound("Client not found!");
            }

            var clientViewModel = new ClientDetailsViewModel()
            {
                Id = clientResponseDto.Id,
                Name = clientResponseDto.Name,
                Email = clientResponseDto.Email,
                Address = clientResponseDto.Address,
                PhoneNumber = clientResponseDto.PhoneNumber,
                Website = clientResponseDto.Website,
                Country = clientResponseDto.Country,
                RegisteredOn = clientResponseDto.RegisteredOn,
                Promotions = clientResponseDto.Promotions
                    .Select(p => new PromotionViewModel()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        StartDate = p.StartDate,
                        EndDate = p.EndDate,
                        PromotionType = p.PromotionType
                    })
                    .ToList()
            };


            return View(clientViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest("Client ID is mandatory!");
            }
            var editClientDto = await _clientService.GetClientForEditAsync(id.Value);

            if (editClientDto == null)
            {
                return NotFound("Client not found!");
            }

            var clientEditViewModel = new ClientEditViewModel()
            {
                Id = editClientDto.Id,
                Name = editClientDto.Name,
                Email = editClientDto.Email,
                Address = editClientDto.Address,
                PhoneNumber = editClientDto.PhoneNumber,
                Website = editClientDto.Website,
                CountryId = editClientDto.CountryId
            };

            clientEditViewModel.Countries = await LoadCountriesAsync();

            return View(clientEditViewModel);
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
                model.Countries = await LoadCountriesAsync();

                return View(model);
            }

            try
            {
                var requestDto = new EditClientRequestData()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    Website = model.Website,
                    CountryId = model.CountryId
                };

                var result = await _clientService.EditClientAsync(id, requestDto);

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

                model.Countries = await LoadCountriesAsync();

                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<SelectListItem>> LoadCountriesAsync()
        {
            var countries = await _clientService.GetCountriesForDropdownAsync();

            return countries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
        }
    }
}
