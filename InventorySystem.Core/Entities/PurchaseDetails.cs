namespace InventorySystem.Core.Entities
{
    public class PurchaseDetails
    {
        public int PurchaseDetailId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal subTotal { get; set; }


        public int? ProductId { get; set; }
        public int? PurchaseId { get; set; }

        public Product? Product { get; set; }
        public Purchase? Purchase { get; set; }
    }
}
