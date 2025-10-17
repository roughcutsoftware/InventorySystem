namespace InventorySystem.Core.DTOs
{
    public class SalesDto
    {
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }   // <-- add this
        public string? CreatedBy { get; set; }

        public ICollection<SalesDetailsDto>? SaleDetails { get; set; }
    }

    public class SalesDetailsDto
    {
        public int SaleDetailId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }   // <-- add this
    }


}
