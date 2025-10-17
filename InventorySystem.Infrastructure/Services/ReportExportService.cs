using System.Text;
using DinkToPdf;
using DinkToPdf.Contracts;
using InventorySystem.Core.DTOs.Reports;
using InventorySystem.Core.Interfaces.Services;

namespace InventorySystem.Core.Services.Implementations
{
    public class ReportExportService : IReportExportService
    {
        private readonly IReportService _reportService;
        private readonly IConverter _converter;

        public ReportExportService(IReportService reportService, IConverter converter)
        {
            _reportService = reportService;
            _converter = converter;
        }

        // -------------------- Public Methods --------------------

        public byte[] GenerateSalesReportPdf(DateTime from, DateTime to, string? userName)
        {
            var sales = _reportService.GetSalesReport(from, to);
            var html = BuildSalesHtml(sales, from, to, userName);
            return ConvertToPdf(html);
        }

        public byte[] GeneratePurchaseReportPdf(DateTime from, DateTime to, string? userName)
        {
            var purchases = _reportService.GetPurchaseReport(from, to);
            var html = BuildPurchaseHtml(purchases, from, to, userName);
            return ConvertToPdf(html);
        }

        public byte[] GenerateInventoryValuationReportPdf(string? userName)
        {
            var inventory = _reportService.GetInventoryValuationReport();
            var html = BuildInventoryHtml(inventory, userName);
            return ConvertToPdf(html);
        }

        // -------------------- Helper Methods --------------------

        private byte[] ConvertToPdf(string html)
        {
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait
                },
                Objects = {
                    new ObjectSettings
                    {
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            };

            return _converter.Convert(pdf);
        }

        private string BuildHtmlHeader(string title)
        {
            var style = new StringBuilder();
            style.Append("<html><head><style>");
            style.Append("table {width:100%;border-collapse:collapse;font-family:Arial;}");
            style.Append("th,td {border:1px solid #ccc;padding:8px;text-align:center;}");
            style.Append("th {background-color:#f4f4f4;} h2 {text-align:center;}");
            style.Append("</style></head><body>");
            style.Append($"<h2>{title}</h2>");
            return style.ToString();
        }

        private string BuildSignatureSection(string? userName)
        {
            return $"<br/><br/><div style='text-align:right;margin-top:50px;'>" +
                   $"<p>Signature:</p>" +
                   $"<p style='margin-top:40px;font-family:Cursive;font-size:18px;'><b>{userName ?? "System User"}</b></p>" +
                   $"</div></body></html>";
        }

        // -------------------- Report Builders --------------------

        private string BuildSalesHtml(List<SalesReportDto> sales, DateTime from, DateTime to, string? userName)
        {
            var html = new StringBuilder();
            html.Append(BuildHtmlHeader("Sales Invoice Report"));
            html.Append($"<p style='text-align:center;'>From {from:yyyy-MM-dd} To {to:yyyy-MM-dd}</p>");
            html.Append("<table><tr><th>Product</th><th>Customer</th><th>Quantity</th><th>Unit Price</th><th>Total</th><th>Date</th></tr>");

            decimal total = 0;
            foreach (var s in sales)
            {
                total += s.Subtotal;
                html.Append($"<tr><td>{s.ProductName}</td><td>{s.CustomerName}</td><td>{s.Quantity}</td><td>{s.UnitPrice:C}</td><td>{s.Subtotal:C}</td><td>{s.SaleDate:yyyy-MM-dd}</td></tr>");
            }

            html.Append($"<tr><td colspan='4' style='text-align:right;'><b>Grand Total:</b></td><td colspan='2'><b>{total:C}</b></td></tr>");
            html.Append("</table>");
            html.Append(BuildSignatureSection(userName));

            return html.ToString();
        }

        private string BuildPurchaseHtml(List<PurchaseReportDto> purchases, DateTime from, DateTime to, string? userName)
        {
            var html = new StringBuilder();
            html.Append(BuildHtmlHeader("Purchase Invoice Report"));
            html.Append($"<p style='text-align:center;'>From {from:yyyy-MM-dd} To {to:yyyy-MM-dd}</p>");
            html.Append("<table><tr><th>Product</th><th>Supplier</th><th>Quantity</th><th>Unit Cost</th><th>Total</th><th>Date</th></tr>");

            decimal total = 0;
            foreach (var p in purchases)
            {
                total += p.Subtotal;
                html.Append($"<tr><td>{p.ProductName}</td><td>{p.SupplierName}</td><td>{p.Quantity}</td><td>{p.UnitCost:C}</td><td>{p.Subtotal:C}</td><td>{p.PurchaseDate:yyyy-MM-dd}</td></tr>");
            }

            html.Append($"<tr><td colspan='4' style='text-align:right;'><b>Grand Total:</b></td><td colspan='2'><b>{total:C}</b></td></tr>");
            html.Append("</table>");
            html.Append(BuildSignatureSection(userName));

            return html.ToString();
        }

        private string BuildInventoryHtml(List<InventoryValuationDto> inventory, string? userName)
        {
            var html = new StringBuilder();
            html.Append(BuildHtmlHeader("Inventory Valuation Report"));
            html.Append($"<p style='text-align:center;'>As of {DateTime.Now:yyyy-MM-dd}</p>");
            html.Append("<table><tr><th>Product</th><th>Category</th><th>Quantity in Stock</th><th>Cost Price</th><th>Total Value</th></tr>");

            decimal grandTotal = 0;
            foreach (var item in inventory)
            {
                grandTotal += item.TotalValue;
                html.Append($"<tr><td>{item.ProductName}</td><td>{item.CategoryName}</td><td>{item.QuantityInStock}</td><td>{item.CostPrice:C}</td><td>{item.TotalValue:C}</td></tr>");
            }

            html.Append($"<tr><td colspan='4' style='text-align:right;'><b>Grand Total:</b></td><td><b>{grandTotal:C}</b></td></tr>");
            html.Append("</table>");
            html.Append(BuildSignatureSection(userName));

            return html.ToString();
        }
    }
}
