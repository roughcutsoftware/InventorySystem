using InventorySystem.Core.DTOs;
using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces;
using InventorySystem.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventorySystem.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Roles = "Admin")]
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService _purchaseService;
        private readonly ILogger<PurchaseController> _logger;
        private readonly ISupplierService _supplierService;
        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;


        public PurchaseController(IPurchaseService purchaseService, ILogger<PurchaseController> logger,ISupplierService supplierService, IProductService productService, UserManager<ApplicationUser> userManager)
        {
            _purchaseService = purchaseService;
            _logger = logger;
            _supplierService = supplierService;
            _productService = productService;
            _userManager = userManager;
        }

        public IActionResult Index(int size = 5, int pageNumber = 1)
        {
            try
            {
                var purchases = _purchaseService.GetAllPurchases(size, pageNumber);
                var dto = new PaginationDto<PurchaseOrderDto>
                {
                    PageNumber = purchases.PageNumber,
                    PageSize = purchases.PageSize,
                    TotalCount = purchases.TotalCount,
                    Items = purchases.Items
                       .Select(p => new PurchaseOrderDto
                       {
                           PurchaseId = p.PurchaseId,
                           PurchaseDate = p.PurchaseDate,
                           Status = p.Status,
                           TotalAmount = p.TotalAmount
                       })
                      .ToList()
                };

                _logger.LogInformation("Fetched {count} purchases for page {page}", purchases.TotalCount, pageNumber);
                return View(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load purchases.");
                TempData["Error"] = "Unable to load purchases.";
                return View(new List<PurchaseOrderDto>());
            }
        }

        public IActionResult Details(int id)
        {
            var purchase = _purchaseService.GetPurchaseById(id);
            if (purchase == null)
            {
                _logger.LogWarning("Purchase #{id} not found.", id);
                return NotFound();
            }
            return View(purchase);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Suppliers = new SelectList(_supplierService.GetAllSuppliers().Items, "SupplierId", "Name");
            ViewBag.Products = _productService.GetAllProducts().Items;
            return View(new PurchaseOrderDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseOrderDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Suppliers = _supplierService.GetAllSuppliers().Items;
                ViewBag.Products = _productService.GetAllProducts().Items;
                return View(dto);
            }

            try
            {
                var user = await _userManager.GetUserAsync(User);
                dto.CreatedById = user?.Id;

                _purchaseService.CreatePurchaseOrder(dto);
                TempData["Success"] = "Purchase order created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating purchase order.");
                TempData["Error"] = "Failed to create purchase order.";
                ViewBag.Suppliers = _supplierService.GetAllSuppliers().Items;
                ViewBag.Products = _productService.GetAllProducts().Items;
                return View(dto);
            }
        }

        [HttpPost]
        public IActionResult Receive(int id)
        {
            try
            {
                _purchaseService.ReceiveStock(id);
                TempData["Success"] = "Stock received successfully!";
                _logger.LogInformation("Stock received for Purchase #{id}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error receiving stock for Purchase #{id}", id);
                TempData["Error"] = "Failed to receive stock.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Cancel(int id)
        {
            try
            {
                _purchaseService.CancelPurchase(id);
                TempData["Success"] = "Purchase cancelled successfully!";
                _logger.LogInformation("Cancelled Purchase #{id}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cancelling Purchase #{id}", id);
                TempData["Error"] = "Failed to cancel purchase.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
