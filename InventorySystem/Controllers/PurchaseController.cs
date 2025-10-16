using InventorySystem.Core.DTOs;
using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Web.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService _purchaseService;
        private readonly ILogger<PurchaseController> _logger;

        public PurchaseController(IPurchaseService purchaseService, ILogger<PurchaseController> logger)
        {
            _purchaseService = purchaseService;
            _logger = logger;
        }

        public IActionResult Index(int page = 1, int size = 20)
        {
            try
            {
                var purchases = _purchaseService.GetAllPurchases(size, page);
                _logger.LogInformation("Fetched {count} purchases for page {page}", purchases.Count(), page);
                return View(purchases);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load purchases.");
                TempData["Error"] = "Unable to load purchases.";
                return View(new List<Purchase>());
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

        public IActionResult Create()
        {
            return View(new PurchaseOrderDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PurchaseOrderDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid purchase order data submitted.");
                return View(dto);
            }

            try
            {
                _purchaseService.CreatePurchaseOrder(dto);
                TempData["Success"] = "Purchase order created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating purchase order.");
                TempData["Error"] = "Failed to create purchase order.";
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
