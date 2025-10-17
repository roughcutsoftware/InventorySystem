using InventorySystem.Core.Interfaces.Services;
using InventorySystem.Web.View_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Web.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IReportExportService _reportExportService;

        public ReportsController(IReportService reportService, IReportExportService reportExportService)
        {
            _reportService = reportService;
            _reportExportService = reportExportService;
        }

        // -------------------- View Pages --------------------

        [HttpGet]
        public IActionResult GenerateReport() => View();

        [HttpGet]
        public IActionResult SalesReport() => View();

        [HttpGet]
        public IActionResult PurchaseReport() => View();

        [HttpGet]
        public IActionResult InventoryValuationReport() => View();

        // -------------------- Data Reports (List Views) --------------------

        [HttpPost]
        public IActionResult SalesReportData(ReportViewModel reportView)
        {
            if (!ModelState.IsValid)
                return View("SalesReport");

            var data = _reportService.GetSalesReport(reportView.DateFrom, reportView.DateTo);
            reportView.Sales = data;
            return View("SalesReport", reportView);
        }

        [HttpPost]
        public IActionResult PurchaseReportData(ReportViewModel reportView)
        {
            if (!ModelState.IsValid)
                return View("PurchaseReport");

            var data = _reportService.GetPurchaseReport(reportView.DateFrom, reportView.DateTo);
            reportView.Purchases = data;
            return View("PurchaseReport", reportView);
        }

        [HttpGet]
        public IActionResult InventoryValuationReportData()
        {
            return View("InventoryValuationReport", _reportService.GetInventoryValuationReport());
        }

        // -------------------- Export PDF Actions --------------------

        [HttpPost]
        public IActionResult ExportSalesReport(ReportViewModel reportView)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("SalesReport");

            var file = _reportExportService.GenerateSalesReportPdf(
                reportView.DateFrom,
                reportView.DateTo,
                User.Identity?.Name
            );

            return File(file, "application/pdf", "SalesReport.pdf");
        }

        [HttpPost]
        public IActionResult ExportPurchaseReport(ReportViewModel reportView)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("PurchaseReport");

            var file = _reportExportService.GeneratePurchaseReportPdf(
                reportView.DateFrom,
                reportView.DateTo,
                User.Identity?.Name
            );

            return File(file, "application/pdf", "PurchaseReport.pdf");
        }

        [HttpGet]
        public IActionResult ExportInventoryValuationReport()
        {
            var file = _reportExportService.GenerateInventoryValuationReportPdf(User.Identity?.Name);
            return File(file, "application/pdf", "InventoryValuationReport.pdf");
        }
    }
}
