using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Services;
using InventorySystem.Core.DTOs;
using Microsoft.AspNetCore.Identity;

namespace InventorySystem.Infrastructure.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogService _logService;

        public UserProfileService(UserManager<ApplicationUser> userManager, ILogService logService)
        {
            _userManager = userManager;
            _logService = logService;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<ProfileResult> UpdateProfileAsync(ProfileDto model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ProfileResult
                {
                    Succeeded = false,
                    Errors = { "User not found" }
                };
            }

            // Update user properties
            user.UserName = model.FullName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Bio = model.Bio;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                _logService.LogAction(user.UserName, "ProfileUpdate", "User profile updated successfully.");
                return new ProfileResult { Succeeded = true };
            }

            return new ProfileResult
            {
                Succeeded = false,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }

        public async Task<SettingsResult> UpdateSettingsAsync(SettingsDto model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new SettingsResult
                {
                    Succeeded = false,
                    Errors = { "User not found" }
                };
            }

            var errors = new List<string>();

            // Change password if provided
            if (!string.IsNullOrEmpty(model.CurrentPassword) && !string.IsNullOrEmpty(model.NewPassword))
            {
                var changePasswordResult = await _userManager.ChangePasswordAsync(
                    user, model.CurrentPassword, model.NewPassword);

                if (!changePasswordResult.Succeeded)
                {
                    errors.AddRange(changePasswordResult.Errors.Select(e => e.Description));
                }
                else
                {
                    _logService.LogAction(user.UserName, "PasswordChange", "User password changed successfully.");
                }
            }

            return new SettingsResult
            {
                Succeeded = errors.Count == 0,
                Errors = errors
            };
        }
    }
}