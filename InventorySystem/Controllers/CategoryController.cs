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
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        public IActionResult Index(int size = 3, int pageNumber = 1)
        {
            var categories = _categoryService.GetAllCategories(size, pageNumber);
            return View(categories);
        }

        public IActionResult Details(int id )
        {
            var categories = _categoryService.GetCategoryById(id);
            if(categories == null) return NotFound();
            return View(categories);
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
