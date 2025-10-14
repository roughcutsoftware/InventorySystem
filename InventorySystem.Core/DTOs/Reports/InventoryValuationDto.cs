using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.DTOs.Reports
{
    public class InventoryValuationDto
    {
        public string ProductName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public int QuantityInStock { get; set; }
        public decimal CostPrice { get; set; }
        public decimal TotalValue => QuantityInStock * CostPrice;
    }
}

