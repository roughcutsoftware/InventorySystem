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
    public class InventoryService : IInventoryService
    {
        private readonly IProductRepository productRepo;
        private readonly IPurchaseDetailRepository purchaseRepo;
        private readonly ISaleDetailRepository salesRepo;
        private readonly IMapper mapper;

        

        public InventoryService(
            IProductRepository productRepo,
            IPurchaseDetailRepository purchaseRepo,
            ISaleDetailRepository salesRepo,
            IMapper mapper)
        {
           this.productRepo = productRepo;
            this.purchaseRepo = purchaseRepo;
            this.salesRepo = salesRepo;
            this.mapper = mapper;
        }

        public List<ProductStockDto> GetStockLevels()
        {
            var products = productRepo.GetAllWithCategoryAndSupplier();
            return mapper.Map<List<ProductStockDto>>(products);
        }

        public List<ProductStockDto> GetLowStockItems()
        {
            var products = productRepo.GetAllWithCategoryAndSupplier();
            var lowStockProducts = products
                .Where(p => p.ReorderLevel.HasValue && p.QuantityInStock <= p.ReorderLevel.Value)
                .ToList();

            return mapper.Map<List<ProductStockDto>>(lowStockProducts);
        }

        public void AdjustStock(int productId, int quantity)
        {
            var product = productRepo.GetByID(productId);
            if (product == null)
                throw new Exception("Product not found");

            product.QuantityInStock += quantity;
            productRepo.Update(product);

            
        }

        public List<InventoryReportDto> GenerateStockReport(DateTime from, DateTime to)
        {
            var purchases = purchaseRepo.GetPurchasesWithinDateRange(from, to);
            var sales = salesRepo.GetSalesWithinDateRange(from, to);
            var products = productRepo.GetAllWithCategoryAndSupplier();

            var report = products.Select(p =>
            {
                var purchasedQty = purchases.Where(pd => pd.ProductId == p.ProductId).Sum(pd => pd.Quantity);
                var soldQty = sales.Where(sd => sd.ProductId == p.ProductId).Sum(sd => sd.Quantity);
                var totalpurchaseamount = purchases.Where(pd => pd.ProductId == p.ProductId).Sum(pd => pd.subTotal);
                var totalsalesamount = sales.Where(sd => sd.ProductId == p.ProductId).Sum(sd => sd.Subtotal);
                var profit = totalsalesamount - totalpurchaseamount;
                return new InventoryReportDto
                {
                    Name = p.Name,
                    QuantityPurchased = purchasedQty,
                    QuantitySold = soldQty,
                    CurrentStock = p.QuantityInStock,
                    TotalPurchaseAmount = totalpurchaseamount,
                    TotalSalesAmount = totalsalesamount,
                    Profit = profit
                };
            }).ToList();

            return report;
        }
    }
}
