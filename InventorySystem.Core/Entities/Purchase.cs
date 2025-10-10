using System;
using System.Collections.Generic;

namespace InventorySystem.Core.Entities
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; } 

        public string? CreatedById { get; set; }
        public int SupplierId { get; set; }

        public ApplicationUser? CreatedBy { get; set; }
        public Supplier Supplier { get; set; } = null!;
        public ICollection<PurchaseDetails> PurchaseDetails { get; set; } = new HashSet<PurchaseDetails>();
    }
}
