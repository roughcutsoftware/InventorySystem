using InventorySystem.Core.DTOs;
using InventorySystem.Core.Entities;

namespace InventorySystem.Core.Interfaces.Services
{
    public interface ISalesService
    {
        void CreateSalesOrder(SalesDto dto);
        IEnumerable<Sales> GetAllSales(int size, int pageNumber);
        SalesDto? GetSalesById(int id);
        void ReduceStockAsync(int productId, int quantity); 
        void CancelSaleAsync(int salesId);
    }
}
