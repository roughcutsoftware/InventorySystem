using InventorySystem.Core.DTOs.Inventory;
using InventorySystem.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.web.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IInventoryService inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            this.inventoryService = inventoryService;
        }

        public IActionResult Index()
        {
            var productStockLevels = inventoryService.GetStockLevels();
            return View("ShowStockLevels",productStockLevels);
        }

        public IActionResult LowStock()
        {
            var lowStockItems = inventoryService.GetLowStockItems();
            return View(lowStockItems);
        }

        [HttpGet]
        public IActionResult AdjustStock()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdjustStock(AdjustStockDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var userName = User.Identity?.Name;
            inventoryService.AdjustStock(dto.ProductId, dto.Quantity, dto.Reason, userName);

            TempData["SuccessMessage"] = "Stock adjusted successfully.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult StockReport(DateTime? from, DateTime? to)
        {
            var startDate = from ?? DateTime.Now.AddMonths(-1);
            var endDate = to ?? DateTime.Now;
            var report = inventoryService.GenerateStockReport(startDate, endDate);
            return View(report);
        }
    }
}
