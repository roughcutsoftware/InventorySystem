using InventorySystem.Core.DTOs.User;
using InventorySystem.Core.Interfaces.Services;
using InventorySystem.web.View_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventorySystem.web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.LoginAsync(
                    loginViewModel.Email,
                    loginViewModel.Password,
                    loginViewModel.RememberMe);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", result.ErrorMessage);
            }

            return View("Login", loginViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
           
            return View(new UserCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
               
                model.AvailableRoles = new List<SelectListItem>
                {
                    new SelectListItem { Value = "User", Text = "Normal User" },
                    new SelectListItem { Value = "Admin", Text = "Administrator" }
                };
                return View(model);
            }

            
            var dto = new RegisterDto
            {
                UserName = model.Username,
                Email = model.Email,
                Password = model.Password,
                Role = model.SelectedRole
            };

            var result = await _authService.RegisterAsync(dto);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

           
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

           
            model.AvailableRoles = new List<SelectListItem>
            {
                new SelectListItem { Value = "User", Text = "Normal User" },
                new SelectListItem { Value = "Admin", Text = "Administrator" }
            };

            return View(model);
        }
    }
}
