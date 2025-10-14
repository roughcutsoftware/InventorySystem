using InventorySystem.Core.Entities;

namespace InventorySystem.Core.Interfaces.Services
{
    public interface INotificationService
    {
        Task NotifyLowStock(Product product);
        Task NotifyPurchaseReceived();
    }
}
