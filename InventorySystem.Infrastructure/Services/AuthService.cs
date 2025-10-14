using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
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

       
    }
}
