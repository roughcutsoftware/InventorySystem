using InventorySystem.Core.DTOs.Reports;
using InventorySystem.Core.Interfaces.Services;
using InventorySystem.Infrastructure.Data; 
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Core.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly AppDBContext _context;

        public ReportService(AppDBContext context)
        {
            _context = context;
        }

        public List<SalesReportDto> GetSalesReport(DateTime from, DateTime to)
        {
            var data = _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.SaleDetails)
                    .ThenInclude(sd => sd.Product)
                .Where(s => s.SaleDate >= from && s.SaleDate <= to)
                .SelectMany(s => s.SaleDetails.Select(d => new SalesReportDto
                {
                    SaleDate = s.SaleDate,
                    CustomerName = s.Customer!.Name!,
                    ProductName = d.Product!.Name!,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice
                }))
                .ToList();

            return data;
        }

        public List<PurchaseReportDto> GetPurchaseReport(DateTime from, DateTime to)
        {
            var data = _context.Purchases
                .Include(p => p.Supplier)
                .Include(p => p.PurchaseDetails)
                    .ThenInclude(pd => pd.Product)
                .Where(p => p.PurchaseDate >= from && p.PurchaseDate <= to)
                .SelectMany(p => p.PurchaseDetails.Select(d => new PurchaseReportDto
                {
                    PurchaseDate = p.PurchaseDate,
                    SupplierName = p.Supplier!.Name!,
                    ProductName = d.Product!.Name!,
                    Quantity = d.Quantity,
                    UnitCost = d.UnitCost
                }))
                .ToList();

            return data;
        }

        public List<InventoryValuationDto> GetInventoryValuationReport()
        {
            var data = _context.Products
                .Include(p => p.Category)
                .Select(p => new InventoryValuationDto
                {
                    ProductName = p.Name!,
                    CategoryName = p.Category!.Name!,
                    QuantityInStock = p.QuantityInStock,
                    CostPrice = p.CostPrice
                })
                .ToList();

            return data;
        }
    }
}
