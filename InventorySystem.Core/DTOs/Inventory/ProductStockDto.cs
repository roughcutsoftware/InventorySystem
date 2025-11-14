using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.DTOs.Inventory
{
    public class ProductStockDto
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public int QuantityInStock { get; set; }
        public int? ReorderLevel { get; set; }
        public string CategoryName { get; set; }
        public string SupplierName { get; set; }
    }
}
