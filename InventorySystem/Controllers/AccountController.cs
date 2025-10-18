using InventorySystem.Core.DTOs.User;
using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Services;
using InventorySystem.web.View_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            if(ModelState.IsValid)
            {
                var result = await _authService.LoginAsync(
                    loginViewModel.Email,
                    loginViewModel.Password,
                    loginViewModel.RememberMe);
                if (result.Succeeded) {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", result.ErrorMessage);
            }
            return View("Login",loginViewModel);
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
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var dto = new RegisterDto
            {
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
                Role = model.Role
            };

            var result = await _authService.RegisterAsync(model);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

    }
}
