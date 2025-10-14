using System.Text;
using DinkToPdf;
using DinkToPdf.Contracts;
using InventorySystem.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportService _salesReportService;
        private readonly IConverter _converter;

        public ReportsController(IReportService salesReportService, IConverter converter)
        {
            _salesReportService = salesReportService;
            _converter = converter;
        }


        [HttpGet]
        public IActionResult GenerateReport()
            => View();


            [HttpPost]
        public IActionResult SalesReport(ReportViewModel reportView)
        {
            if(ModelState.IsValid)
            {
                var sales = _salesReportService.GetSalesReport(reportView.DateFrom, reportView.DateTo);

                StringBuilder html = new StringBuilder();
                html.Append("<html><head>");
                html.Append("<style>");
                html.Append("table {width:100%;border-collapse:collapse;font-family:Arial;}");
                html.Append("th,td {border:1px solid #ccc;padding:8px;text-align:center;}");
                html.Append("th {background-color:#f4f4f4;}");
                html.Append("h2 {text-align:center;}");
                html.Append("</style></head><body>");
                html.Append("<h2>Sales Invoice Report</h2>");
                html.Append($"<p style='text-align:center;'>From {reportView.DateFrom:yyyy-MM-dd} To {reportView.DateTo:yyyy-MM-dd}</p>");
                html.Append("<table><tr><th>Product</th><th>Customer</th><th>Quantity</th><th>Unit Price</th><th>Total</th><th>Date</th></tr>");


                decimal total = 0;
                foreach (var s in sales)
                {
                    total += s.Subtotal;
                    html.Append($"<tr><td>{s.ProductName}</td><td>{s.CustomerName}</td><td>{s.Quantity}</td><td>{s.UnitPrice:C}</td><td>{s.Subtotal:C}</td><td>{s.SaleDate:yyyy-MM-dd}</td></tr>");
                }
                html.Append($"<tr><td colspan='4' style='text-align:right;'><b>Grand Total:</b></td><td colspan='2'><b>{total:C}</b></td></tr>");
                html.Append("</table>");

                html.Append("<br/><br/><br/><br/>");

                html.Append("<div style='text-align:right;margin-top:50px;'>");
                html.Append("<p>Signature:</p>");
                html.Append("<p style='margin-top:40px;font-family:Cursive;font-size:18px;'><b>3laa Erfan</b></p>");
                html.Append("</div>");

                html.Append("</body></html>");


                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait
                },
                    Objects = {
                    new ObjectSettings {
                        HtmlContent = html.ToString(),
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
                };

                byte[] file = _converter.Convert(pdf);
                return File(file, "application/pdf", "SalesReport.pdf");
            }
            return RedirectToAction("GenerateReport", reportView);
        }
    }
}
