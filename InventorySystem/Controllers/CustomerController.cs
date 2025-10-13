using InventorySystem.Core.DTOs;
using InventorySystem.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public IActionResult Index(int size = 20, int pageNumber = 1)
        {
            var customer = customerService.GetAllCustomers(size, pageNumber);
            return View(customer);
        }

        public IActionResult Details(int id)
        {
            var customer = customerService.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                return View(customer);
            }
        }

        public IActionResult Add()
        {
            var vm = new Customer_ViewModel()
            {
                Customer = new CustomerDto() { }
            };
            return View(vm);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddNewCustomer(Customer_ViewModel vm)
        {
            if (ModelState.IsValid)
            {
                customerService.AddCustomer(vm.Customer);
                return RedirectToAction("Index");
            }
            return View("Add", vm);
        }

        public IActionResult Edit(int id)
        {
            var customer = customerService.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                var vm = new Customer_ViewModel()
                {
                    Customer = customer
                };
                return View(vm);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult EditCustomer(Customer_ViewModel vm)
        {
            if (ModelState.IsValid)
            {
                customerService.UpdateCustomer(vm.Customer);
                return RedirectToAction("Index");
            }
            return View("Edit", vm);
        }

        public IActionResult Delete(int id)
        {
            var customer = customerService.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                return View(customer);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeleteCustomer (int CustomerId)
        {
            customerService.DeleteCustomer(CustomerId);
            return RedirectToAction("Index");
        }
    }
}
