using InventorySystem.Core.DTOs;
using InventorySystem.Core.Entities;

namespace InventorySystem.Core.Interfaces.Services
{
    public interface ISalesService
    {
        void CreateSalesOrder(SalesDto dto);
        PaginationDto<Sales> GetAllSales(int size, int pageNumber);
        SalesDto? GetSalesById(int id);
        void ReduceStock(int productId, int quantity); 
        void CancelSale(int salesId);
        void MarkAsCompleted(int id);
    }
}
