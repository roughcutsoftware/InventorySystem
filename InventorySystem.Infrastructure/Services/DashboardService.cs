using InventorySystem.Core.DTOs;
using InventorySystem.Core.Interfaces.Repositories;
using InventorySystem.Core.Interfaces.Services;

namespace InventorySystem.Infrastructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IProductRepository _productRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ISalesRepository _salesRepository;
        private readonly IPurchaseRepository _purchaseRepository;



        public DashboardService(IProductRepository productRepository,
            ISupplierRepository supplierRepository,
            ISalesRepository salesRepository,
            IPurchaseRepository purchaseRepository)
        {
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
            _salesRepository = salesRepository;
            _purchaseRepository = purchaseRepository;
        }


        //total product count 

        public int GetTotalProducts()
        {
            return _productRepository.GetAll().Count;
        }

        //total supplierss count 
        public int GetTotalSuppliers()
        {
            return _supplierRepository.GetAll().Count;
        }


        //اجمالي قيمة المبيعات  من و الي
        public decimal GetTotalSales(DateTime from, DateTime to)
        {
            var sales = _salesRepository.GetAll()
                .Where(s => s.SaleDate >= from && s.SaleDate <= to)
                .ToList();

            return sales.Sum(s => s.TotalAmount);
        }


        // اجمالي قيمة المشتريات من و الي
        public decimal GetTotalPurchases(DateTime from, DateTime to)
        {
            var purchases = _purchaseRepository.GetAll()
                .Where(p => p.PurchaseDate >= from && p.PurchaseDate <= to)
                .ToList();

            return purchases.Sum(p => p.TotalAmount);
        }



        // عدد المنتجات اللي مخزونها قليل يعني تحت الليفيل 
        public int GetLowStockCount()
        {
            return _productRepository.GetAll()
                .Count(p => p.QuantityInStock <= p.ReorderLevel);
        }


        public ProfitSummaryDto GetProfitSummary(DateTime from, DateTime to)
        {
            var revenue = GetTotalSales(from, to);
            var cost = GetTotalPurchases(from, to);
            var profit = revenue - cost;
            var percent = revenue != 0 ? (profit / revenue) * 100 : 0;

            return new ProfitSummaryDto
            {
                Revenue = revenue,
                CostOfGoodsSold = cost,
                GrossProfit = profit,
                GrossMarginPercent = Math.Round(percent, 2)
            };

        }

             public DashboardSummaryDto GetDashboardSummary(DateTime from, DateTime to)
        {
            var dashboard = new DashboardSummaryDto
            {
                From = from,
                To = to,
                TotalProducts = GetTotalProducts(),
                TotalSuppliers = GetTotalSuppliers(),
                TotalSales = GetTotalSales(from, to),
                TotalPurchases = GetTotalPurchases(from, to),
                LowStockCount = GetLowStockCount(),
                ProfitSummary = GetProfitSummary(from, to)
            };

            return dashboard;
        }


     
    }
    }

