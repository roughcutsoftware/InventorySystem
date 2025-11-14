using InventorySystem.Core.Entities;
using InventorySystem.Core.DTOs;

namespace InventorySystem.Core.Interfaces.Services
{
    public interface IUserProfileService
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<ProfileResult> UpdateProfileAsync(ProfileDto model, string userId);
        Task<SettingsResult> UpdateSettingsAsync(SettingsDto model, string userId);
    }

    public class ProfileResult
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    public class SettingsResult
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}