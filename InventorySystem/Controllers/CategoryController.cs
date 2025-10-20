using InventorySystem.Core.DTOs;
using InventorySystem.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.web.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        public CategoryController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }


        public IActionResult Index(int size = 5, int pageNumber = 1)
        {
            var categories = _categoryService.GetAllCategories(size, pageNumber);
            return View(categories);
        }

        //public IActionResult Details(int id )
        //{
        //    var categories = _categoryService.GetCategoryById(id);
        //    if(categories == null) return NotFound();
        //    return View(categories);
        //}

        public IActionResult Details(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null) return NotFound();

            // Get products under this category
            var products = _productService.GetProductsByCategoryId(id);

            // Combine category and product info
            var vm = new Category_Details_ViewModel
            {
                Category = category,
                Products = products.ToList()
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Add() { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CategoryDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            _categoryService.AddCategory(dto);
            return RedirectToAction(nameof(Index));
           
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category =_categoryService.GetCategoryById(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            _categoryService.UpdateCategory(dto);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category =_categoryService.GetCategoryById(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int CategoryId)
        {
            _categoryService.DeleteCategory(CategoryId);
            return RedirectToAction(nameof(Index));
        }

    }
}
