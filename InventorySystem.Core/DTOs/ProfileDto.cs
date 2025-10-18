using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Core.DTOs
{
    public class ProfileDto
    {
        [Required(ErrorMessage = "Full name is required")]
        [Display(Name = "Full Name")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format")]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Profile Picture URL")]
        [Url(ErrorMessage = "Invalid URL format")]
        public string? ProfilePicture { get; set; }

        [Display(Name = "Bio")]
        [StringLength(500, ErrorMessage = "Bio cannot exceed 500 characters")]
        public string? Bio { get; set; }
    }
}