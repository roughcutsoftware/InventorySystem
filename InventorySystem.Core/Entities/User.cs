namespace InventorySystem.Core.Entities
{
    public class User
        {
            public int UserId { get; set; }

            public string Username { get; set; } = string.Empty;
            public string PasswordHash { get; set; } = string.Empty;
            public string Role { get; set; } = "User";

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
            //public ICollection<Sales> Sales { get; set; } = new List<Sales>();
    }
}
