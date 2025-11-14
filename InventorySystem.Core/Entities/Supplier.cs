namespace InventorySystem.Core.Entities
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string? Name { get; set; } 
        public string? ContactName { get; set; } 
        public string? CompanyName { get; set; } 
        public string? Email { get; set; } 
        public string? Phone { get; set; } 
        public string? Address { get; set; } 
        public bool IsActive { get; set; }

        public ICollection<Purchase>? Purchases { get; set; } = new HashSet<Purchase>();
        public ICollection<Product>? Products { get; set; } = new HashSet<Product>();

    }
}
