using System.ComponentModel.DataAnnotations;

namespace InventorySystem.web.View_Models
{
    public class SettingsViewModel
    {
        [Display(Name = "Current Password")]
        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }

        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string? NewPassword { get; set; }

        [Display(Name = "Confirm New Password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        [Display(Name = "Email Notifications")]
        public bool EmailNotifications { get; set; } = true;

        [Display(Name = "SMS Notifications")]
        public bool SmsNotifications { get; set; }

        [Display(Name = "Language")]
        public string Language { get; set; } = "English";

        [Display(Name = "Time Zone")]
        public string TimeZone { get; set; } = "UTC";
    }
}