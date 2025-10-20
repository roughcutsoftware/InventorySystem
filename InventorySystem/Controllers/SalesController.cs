using InventorySystem.Core.DTOs;
using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Services;
using InventorySystem.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventorySystem.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize]
    public class SalesController : Controller
    {
        private readonly ISalesService _salesService;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;

        public SalesController(
            ISalesService salesService,
            ICustomerService customerService,
            IProductService productService,
            UserManager<ApplicationUser> userManager)
        {
            _salesService = salesService;
            _customerService = customerService;
            _productService = productService;
            _userManager = userManager;
        }

        public IActionResult Index(int size = 5, int pageNumber = 1)
        {
            var salesPage = _salesService.GetAllSales(size, pageNumber);

            var dto = new PaginationDto<SalesDto>
            {
                PageNumber = salesPage.PageNumber,
                PageSize = salesPage.PageSize,
                TotalCount = salesPage.TotalCount,
                Items = salesPage.Items.Select(s => new SalesDto
                {
                    SaleId = s.SaleId,
                    SaleDate = s.SaleDate,
                    CustomerName = s.Customer?.Name,
                    TotalAmount = s.TotalAmount,
                    Status = s.Status
                }).ToList()
            };

            return View(dto);
        }

        public IActionResult Details(int id)
        {
            var sale = _salesService.GetSalesById(id);
            if (sale == null)
                return NotFound();

            return View(sale);
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadDropdowns();
            return View(new SalesDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalesDto dto)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns();
                return View(dto);
            }

            var user = await _userManager.GetUserAsync(User);
            dto.CreatedBy = user?.Id;

            try
            {
                _salesService.CreateSalesOrder(dto);
                return RedirectToAction(nameof(Index));
             }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                LoadDropdowns();
                return View(dto);
    }
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
        private void LoadDropdowns()
        {
            var customers = _customerService.GetAllCustomers().Items;
            var products = _productService.GetAllProducts();

            ViewBag.Customers = new SelectList(customers, "CustomerId", "Name");
            ViewBag.Products = products;
        }
    }
}
