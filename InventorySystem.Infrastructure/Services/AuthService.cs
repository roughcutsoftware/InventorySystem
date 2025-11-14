
using InventorySystem.Core.DTOs.User;
using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace InventorySystem.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogService _logService;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogService logService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logService = logService;
        }

        public async Task<LoginResult> LoginAsync(string email, string password, bool rememberMe)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                bool found = await _userManager.CheckPasswordAsync(user, password);
                if (found)
                {
                    await _signInManager.SignInAsync(user, rememberMe);
                    _logService.LogAction(user.UserName, "Login", "User logged in successfully.");
                    return new LoginResult { Succeeded = true, User = user };
                }
            }
            return new LoginResult {
                Succeeded = false,
                ErrorMessage = "Invalid credentials"
            };
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<IdentityResult> RegisterAsync(RegisterDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                CreatedAt = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
               
                await _userManager.AddToRoleAsync(user, model.Role);

               
                await _signInManager.SignInAsync(user, isPersistent: false);

                _logService.LogAction(user.UserName, "Register", $"User registered with role: {model.Role}");
            }

            return result;
        }

        public async Task RefreshSignInAsync(ApplicationUser user)
        {
            await _signInManager.RefreshSignInAsync(user);
        }


       
    }
}
