using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? Name { get; set; } 
        public int QuantityInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal CostPrice { get; set; }
        public int? ReorderLevel { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedAt { get; set; } 


        public int CategoryId { get; set; }
        public int SupplierId { get; set; }


        public Category Category { get; set; } = null!;
        public Supplier Supplier { get; set; } = null!;
        public ICollection<SaleDetails> SaleDetails { get; set; } = new HashSet<SaleDetails>();
        public ICollection<PurchaseDetails> PurchaseDetails { get; set; } = new HashSet<PurchaseDetails>();
    }


}



