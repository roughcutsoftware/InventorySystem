namespace InventorySystem.Core.Entities
{
    public class SaleDetails
    {
        public int SaleDetailId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }


        public int? ProductId { get; set; }
        public int? SaleId { get; set; }


        public Product Product { get; set; } = null!;
        public Sales Sales { get; set; } = null!;
    }


}
