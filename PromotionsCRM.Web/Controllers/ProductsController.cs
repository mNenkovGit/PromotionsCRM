using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PromotionsCRM.Common;
using PromotionsCRM.Services.DTOs.Product.Request;
using PromotionsCRM.Services.Interfaces;
using PromotionsCRM.ViewModels.Products;

namespace PromotionsCRM.Web.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [Authorize(Roles = RoleNames.Administrator)]
        [HttpGet]
        public async Task<IActionResult> Create(int? promoId)
        {
            if (promoId == null || promoId <= 0)
            {
                return BadRequest();
            }

            var promotionName = await _productService.GetPromotionNameAsync(promoId.Value);

            if (promotionName == null)
            {
                return NotFound();
            }

            var newProduct = new ProductCreateViewModel()
            {
                PromotionId = promoId.Value,
                PromotionName = promotionName
            };

            return View(newProduct);
        }

        [Authorize(Roles = RoleNames.Administrator)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel createModel)
        {
            string? promotionName = await _productService.GetPromotionNameAsync(createModel.PromotionId);

            if (promotionName is null)
            {
                return BadRequest();
            }

            if (!string.IsNullOrWhiteSpace(createModel.Name) && await _productService.ProductNameExistsAsync(createModel.PromotionId, createModel.Name))
            {
                ModelState.AddModelError(nameof(createModel.Name), "This promotion already has a product with this name!");
            }

            if (!ModelState.IsValid)
            {
                createModel.PromotionName = promotionName;
                return View(createModel);
            }

            try
            {
                var creatProductDto = new CreateProductRequestDto()
                {
                    Name = createModel.Name,
                    PromotionId = createModel.PromotionId
                };
                await _productService.CreateProductAsync(creatProductDto);

                TempData[NotificationKeys.SuccessMessage] = "Product added successfully!";

                return RedirectToAction(nameof(PromotionsController.Details), "Promotions", new { id = createModel.PromotionId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the product!");

                createModel.PromotionName = promotionName;
                TempData[NotificationKeys.ErrorMessage] = "An error occurred while creating the product. Please try again later!";

                return View(createModel);
            }
        }
    }
}
