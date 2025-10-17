
using InventorySystem.Core.DTOs;
using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Web.Controllers
{
    public class SalesController : Controller
    {
        private readonly ISalesService _salesService;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;

        public SalesController(ISalesService salesService, ICustomerService customerService, IProductService productService)
        {
            _salesService = salesService;
            _customerService = customerService;
            _productService = productService;
        }

        // GET: /Sales
        public IActionResult Index(int page = 1)
        {
            var sales = _salesService.GetAllSales(20, page);
            return View(sales);
        }

        // GET: /Sales/Details/5
        public IActionResult Details(int id)
        {
            var sale = _salesService.GetSalesById(id);
            if (sale == null) return NotFound();
            return View(sale);
        }

        // GET: /Sales/Create
        public IActionResult Create()
        {
            ViewBag.Customers = _customerService.GetAllCustomers();
            ViewBag.Products = _productService.GetAllProducts();
            return View(new SalesDto { SaleDetails = new List<SalesDetailsDto>() });
        }

        // POST: /Sales/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SalesDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Customers = _customerService.GetAllCustomers();
                ViewBag.Products = _productService.GetAllProducts();
                return View(dto);
            }

            _salesService.CreateSalesOrder(dto);
            return RedirectToAction(nameof(Index));
        }

        // POST: /Sales/Cancel/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel(int id)
        {
            _salesService.CancelSale(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

