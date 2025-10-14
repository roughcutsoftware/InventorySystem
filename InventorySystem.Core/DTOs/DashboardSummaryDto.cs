
namespace InventorySystem.Core.DTOs
{
    public class DashboardSummaryDto
    {
        public int TotalProducts { get; set; }
        public int TotalSuppliers { get; set; }        
        public decimal TotalSales { get; set; }        // المبلغ الكلي الي دخللك من المبيعات 
        public decimal TotalPurchases { get; set; }    //المبلغ الي دفعته عشان تشتري بضاعه
        public int LowStockCount { get; set; }          // عدد المنتجات اللي مخزونها قليل أقل من الحد المسموح به
        public ProfitSummaryDto ProfitSummary { get; set; }  // تفاصيل الربح
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
    public class ProfitSummaryDto
    {
        public decimal Revenue { get; set; }      //الإيرادات
        public decimal CostOfGoodsSold { get; set; }  // تكلفة البضاعه الي بعناها .. يعني دفعت كام ععلي البضاعه دي 
        public decimal GrossProfit { get; set; }  //إجمالي الربح = الإيرادات - تكلفة البضائع المباعة.
        public decimal GrossMarginPercent { get; set; }  // نسبة الربح المئوية = (الربح ÷ الإيرادات) × 100، بتوضح هامش الربح كنسبة مئوية.
    }
    public class StockItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int QuantityOnHand { get; set; }  // الكميه الي معانا 
        public int ReorderLevel { get; set; } // الحد الأدنى للمخزون — لو الكمية الحالية أقل منه، لازم تطلب المنتج من المورد.
    }
}
