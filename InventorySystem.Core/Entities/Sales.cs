using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int CreatedBy { get; set; } // UserId

        //Nav props  i commented them until we implement these entities 
        public Customer? Customer { get; set; }
        public ApplicationUser? User { get; set; }
        public ICollection<SaleDetails>? SaleDetails { get; set; }
    }
}
