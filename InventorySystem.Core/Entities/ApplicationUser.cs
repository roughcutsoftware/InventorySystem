using Microsoft.AspNetCore.Identity;

namespace InventorySystem.Core.Entities
{
    public class ApplicationUser : IdentityUser
        {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Purchase>? Purchases { get; set; }
        public ICollection<Sales>? Sales { get; set; }
        public string? Bio { get; set; }
    }
}
