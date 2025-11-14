using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public int QuantityInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal CostPrice { get; set; }
        public int? ReorderLevel { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CategoryId { get; set; }
        public int SupplierId { get; set; }

        public string? CategoryName { get; set; }
        public string? SupplierName { get; set; }
    }
}
