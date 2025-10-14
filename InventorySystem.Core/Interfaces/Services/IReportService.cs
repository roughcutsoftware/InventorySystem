using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorySystem.Core.DTOs.Reports;

namespace InventorySystem.Core.Interfaces.Services
{
    public interface IReportService
    {
        List<SalesReportDto> GetSalesReport(DateTime from, DateTime to);
        List<PurchaseReportDto> GetPurchaseReport(DateTime from, DateTime to);
        List<InventoryValuationDto> GetInventoryValuationReport();
    }
}
