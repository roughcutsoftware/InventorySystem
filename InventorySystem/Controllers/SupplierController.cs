using InventorySystem.Core.DTOs;
using InventorySystem.Core.Interfaces;
using InventorySystem.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace InventorySystem.web.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public IActionResult Index()
        {
            var suppliers = _supplierService.GetAllSuppliers();
            return View(suppliers);
        }
        public IActionResult Details(int id)
        {
            var supplier = _supplierService.GetSupplierById(id);
            if (supplier == null) return NotFound();

            return View(supplier);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(SupplierDto dto)
        {
            if (!ModelState.IsValid) { return View(dto); }
            _supplierService.AddSupplier(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var supplier = _supplierService.GetSupplierById(id);
            if (supplier == null) return NotFound();
            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SupplierDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            _supplierService.UpdateSupplier(dto);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var supplier =_supplierService.GetSupplierById(id);
            if (supplier == null) return NotFound();
            return View(supplier);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSupplier(int id)
        {
            _supplierService.DeleteSupplier(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
