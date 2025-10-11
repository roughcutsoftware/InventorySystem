using InventorySystem.Core.DTOs;
using InventorySystem.Core.Entities;

namespace InventorySystem.Core.Interfaces.Services
{
    public interface IPurchaseService
    {
        void CreatePurchaseOrder(PurchaseOrderDto dto);

        IEnumerable<Purchase> GetAllPurchases(int size, int pageNumber);

        PurchaseOrderDto? GetPurchaseById(int id);

        void ReceiveStock(int purchaseId);

        void CancelPurchase(int id);
    }
}
