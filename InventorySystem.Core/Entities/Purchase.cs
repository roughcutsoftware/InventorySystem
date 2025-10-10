namespace InventorySystem.Core.Entities
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;

        public string? CreatedById { get; set; }
        public int SupplierId { get; set; }

        public ApplicationUser? User { get; set; }

        //public Supplier Supplier { get; set; }

        //public ICollection<PurchaseDetails> PurchaseDetails { get; set; } = new List<PurchaseDetails>();

    }
}
