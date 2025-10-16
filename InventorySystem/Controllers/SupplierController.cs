using InventorySystem.Core.DTOs;
using InventorySystem.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Web.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public IActionResult Index(int page = 1, int size = 20)
        {
            var suppliers = _supplierService.GetAllSuppliers(size, page);
            return View(suppliers);
        }

        public IActionResult Details(int id)
        {
            var supplier = _supplierService.GetSupplierById(id);
            if (supplier == null)
                return NotFound();

            return View(supplier);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SupplierDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            _supplierService.AddSupplier(dto);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var supplier = _supplierService.GetSupplierById(id);
            if (supplier == null)
                return NotFound();

            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SupplierDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            _supplierService.UpdateSupplier(dto);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var supplier = _supplierService.GetSupplierById(id);
            if (supplier == null)
                return NotFound();

            return View(supplier);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _supplierService.DeleteSupplier(id);
            return RedirectToAction(nameof(Index));
        }
    }
}