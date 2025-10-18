using InventorySystem.Core.DTOs.User;
using InventorySystem.Core.Entities;

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<LoginResult> LoginAsync(string email, string password, bool rememberMe);
        Task LogoutAsync();
        Task RefreshSignInAsync(ApplicationUser user);
    }
    public class LoginResult
    {
        public bool Succeeded { get; set; }
        public bool IsLockedOut { get; set; }
        public ApplicationUser User { get; set; }
        public string ErrorMessage { get; set; }
    }
}
