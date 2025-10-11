namespace InventorySystem.Core.DTOs
{
    public class SupplierDto
    {
        public int SupplierId { get; set; }
        public string? Name { get; set; }
        public string? ContactName { get; set; }
        public string? CompanyName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
    }
}
