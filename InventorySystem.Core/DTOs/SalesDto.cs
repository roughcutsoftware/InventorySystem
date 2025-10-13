using InventorySystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.DTOs
{
    public class SalesDto
    {
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }

        
        public int CustomerId { get; set; }
        public string? CreatedBy { get; set; } // UserId

        public ICollection<SaleDetails>? SaleDetails { get; set; } = new HashSet<SaleDetails>();

    }

    public class SalesDetailsDto
    {
        public int SaleDetailId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }


        //public int ProductId { get; set; }
        //public int SaleId { get; set; }


    }
}
