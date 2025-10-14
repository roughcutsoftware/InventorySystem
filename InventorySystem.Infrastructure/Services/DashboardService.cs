using AutoMapper;
using InventorySystem.Core.DTOs;
using InventorySystem.Core.Interfaces.Repositories;
using InventorySystem.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Infrastructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IProductRepository _productRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ISalesRepository _salesRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMapper _mapper;



        public DashboardService(IProductRepository productRepository , 
            ISupplierRepository supplierRepository,
            ISalesRepository salesRepository ,
            IPurchaseRepository purchaseRepository , IMapper mapper)
        {
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
            _salesRepository = salesRepository;
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
        }


        public DashboardSummaryDto GetDashboardSummary(DateTime from, DateTime to)
        {
            //total product count 
            var totalProducts = _productRepository.GetAll().Count();

            //total supplierss count 
            var totalSuppliers = _supplierRepository.GetAll().Count();

            //اجمالي قيمة المبيعات  من و الي  
            var sales = _salesRepository.GetAll()
                .Where(s=>s.SaleDate >= from && s.SaleDate <= to)
                .ToList();
            var totalSalesAmount = sales.Sum(s => s.TotalAmount);


            // اجمالي قيمة المشتريات من و الي 
            var purchases =_purchaseRepository.GetAll()
                .Where(p=>p.PurchaseDate>=from&&p.PurchaseDate<=to)
                .ToList();
            var totalPurchaseAmount = purchases.Sum(p => p.TotalAmount);


            // عدد المنتجات اللي مخزونها قليل يعني تحت الليفيل 
            var lowStockCount = _productRepository.GetAll()
                .Count(p=>p.QuantityInStock <= p.ReorderLevel);


            //حساب الارباح
            var revenue = totalSalesAmount;
            var costOfGoodsSold = totalPurchaseAmount;
            var grossProfit = revenue - costOfGoodsSold;
            var grossMarginPercent = revenue != 0 ? (grossProfit / revenue) * 100 : 0;


            var dashboard = new DashboardSummaryDto {
                From = from,
                To = to,
                TotalProducts = totalProducts,
                TotalSuppliers = totalSuppliers,
                TotalSales = totalSalesAmount,
                TotalPurchases = totalPurchaseAmount,
                LowStockCount = lowStockCount,
                ProfitSummary = new ProfitSummaryDto()
            };
            return dashboard;
        }
    }
}
