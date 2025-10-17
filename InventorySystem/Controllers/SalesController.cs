
using InventorySystem.Core.DTOs;
using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Web.Controllers
{
    public class SalesController : Controller
    {
        private readonly ISalesService _salesService;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;
        public SalesController(ISalesService salesService, ICustomerService customerService, IProductService productService, UserManager<ApplicationUser> userManager   )
        {
            _salesService = salesService;
            _customerService = customerService;
            _productService = productService;
            _userManager = userManager;
        }

        public IActionResult Index(int page = 1)
        {
            var sales = _salesService.GetAllSales(20, page);
            return View(sales);
        }

        public IActionResult Details(int id)
        {
            var sale = _salesService.GetSalesById(id);
            if (sale == null) return NotFound();
            return View(sale);
        }

        public IActionResult Create()
        {
            ViewBag.Customers = _customerService.GetAllCustomers();
            ViewBag.Products = _productService.GetAllProducts();
            return View(new SalesDto { SaleDetails = new List<SalesDetailsDto>() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalesDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Customers = _customerService.GetAllCustomers();
                ViewBag.Products = _productService.GetAllProducts();
                return View(dto);
            }

            var user = await _userManager.GetUserAsync(User);
            dto.CreatedBy = user?.Id;

            _salesService.CreateSalesOrder(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel(int id)
        {
            _salesService.CancelSale(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Complete(int id)
        {
            _salesService.MarkAsCompleted(id);
            return RedirectToAction(nameof(Index));
        }

    }
}

