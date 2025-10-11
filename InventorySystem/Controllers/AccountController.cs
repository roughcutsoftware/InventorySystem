using InventorySystem.web.View_Models;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {

        public AccountController()
        {
            
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {

            return View();
        }

    }
}
