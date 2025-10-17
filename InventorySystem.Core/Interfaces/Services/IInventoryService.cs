using InventorySystem.Core.DTOs.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.Interfaces.Services
{
    public interface IInventoryService
    {
        List<ProductStockDto> GetStockLevels();
        List<ProductStockDto> GetLowStockItems();
        void AdjustStock(int productId, int quantity, string reason,string user );
        List<InventoryReportDto> GenerateStockReport(DateTime from, DateTime to);
    }
}
