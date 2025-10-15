using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.DTOs.Inventory
{
    public class InventoryReportDto
    {
        public string? Name { get; set; }
        public int QuantitySold { get; set; }
        public int QuantityPurchased { get; set; }
        public int CurrentStock { get; set; }
        public decimal TotalSalesAmount { get; set; }
        public decimal TotalPurchaseAmount { get; set; }

        public decimal Profit { get; set; }
    }
}
