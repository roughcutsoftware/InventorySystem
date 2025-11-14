using InventorySystem.Core.Entities;
using InventorySystem.Core.Hubs;
using InventorySystem.Core.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;


namespace InventorySystem.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task NotifyLowStock(Product product)
        {
            if (product == null)
                return;

            string message = $"Low Stock Alert: {product.Name} has only {product.QuantityInStock} units left " +
                             $"(Reorder Level: {product.ReorderLevel ?? 0}).";

            var notification = new
            {
                type = "warning",
                title = "Low Stock",
                message,
                productId = product.ProductId
            };

            Console.WriteLine($"Sending notification: {System.Text.Json.JsonSerializer.Serialize(notification)}");

            await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification);
        }
        public async Task NotifyPurchaseReceived()
        {
            string message = "A new purchase order has been received and stock has been updated.";

            await _hubContext.Clients.All.SendAsync("ReceiveNotification", new
            {
                type = "success",
                title = "Purchase Received",
                message
            });
        }
    }
}
