using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.Entities
{
    public class PurchaseDetails
    {
        public int PurchaseDetailId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal subTotal { get; set; }

        //Fk's 
        public int ProductId { get; set; }
        public int PurchaseId { get; set; }

        //Nav props  i commented them until we implement these entities 
        public Product? Product { get; set; }
        public Purchase? Purchase { get; set; }
    }
}
