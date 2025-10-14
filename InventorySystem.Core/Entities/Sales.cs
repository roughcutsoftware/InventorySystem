namespace InventorySystem.Core.Entities
{
    public class Sales
    {
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }

        //fk's
        public int CustomerId { get; set; }
        public string? CreatedBy { get; set; } // UserId

        public Customer? Customer { get; set; }
        public ApplicationUser? User { get; set; }
        public ICollection<SaleDetails>? SaleDetails { get; set; } = new HashSet<SaleDetails>();
    }
}
