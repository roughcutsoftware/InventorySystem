using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.Interfaces.Services
{
    public interface IReportExportService
    {
        byte[] GenerateSalesReportPdf(DateTime from, DateTime to, string? userName);
        byte[] GeneratePurchaseReportPdf(DateTime from, DateTime to, string? userName);
        byte[] GenerateInventoryValuationReportPdf(string? userName);
    }
}
