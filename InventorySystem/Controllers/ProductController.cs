using AutoMapper;
using InventorySystem.Core.DTOs;
using InventorySystem.Core.Interfaces;
using InventorySystem.Core.Interfaces.Services;
using InventorySystem.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventorySystem.web.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly ISupplierService supplierService;

        public ProductController(IProductService productService, ICategoryService categoryService, ISupplierService supplierService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.supplierService = supplierService;
        }

        public IActionResult Index(int size = 3, int pageNumber = 1)
        {
            var products = productService.GetAllProducts(size, pageNumber);
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return View(product);
            }
        }

        public IActionResult Add()
        {
            var vm = new Product_ViewModel()
            {
                Product = new ProductDto { CreatedAt = DateTime.Now },
                Categories = categoryService.GetAllCategories().Items,
                Suppliers = supplierService.GetAllSuppliers().Items
            };
            return View(vm);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddNewProduct(Product_ViewModel vm)
        {
            if (ModelState.IsValid)
            {
                productService.AddProduct(vm.Product);
                return RedirectToAction("Index");
            }

            vm.Categories = categoryService.GetAllCategories().Items;
            vm.Suppliers = supplierService.GetAllSuppliers().Items;

            return View("Add", vm);
        }

        public IActionResult Edit(int id)
        {
            var product = productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                var vm = new Product_ViewModel()
                {
                    Product = product,
                    Categories = categoryService.GetAllCategories().Items,
                    Suppliers = supplierService.GetAllSuppliers().Items
                };
                return View(vm);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult EditProduct(Product_ViewModel vm)
        {
            if (ModelState.IsValid)
            {
                productService.UpdateProduct(vm.Product);
                return RedirectToAction("Index");
            }

            vm.Categories = categoryService.GetAllCategories().Items;
            vm.Suppliers = supplierService.GetAllSuppliers().Items;

            return View("Edit", vm);
        }

        public IActionResult Delete(int id)
        {
            var product = productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return View(product);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeleteProduct(int ProductId)
        {
            productService.DeleteProduct(ProductId);
            return RedirectToAction("Index");
        }
    }
}
