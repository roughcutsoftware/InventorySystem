namespace InventorySystem.Core.DTOs
{
    public class PurchaseOrderDto
    {
        public int PurchaseId { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; } = "Pending";

        public string? CreatedById { get; set; }
        public int SupplierId { get; set; }

        public List<PurchaseDetailDto> PurchaseDetails { get; set; } = new();
    }
    public class PurchaseDetailDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal subTotal { get; set; }
    }
}
