using Microsoft.AspNetCore.SignalR;

namespace InventorySystem.Core.Hubs
{
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            //await Clients.Caller.SendAsync("ReceiveNotification", new
            //{
            //    type = "info",
            //    title = "Connection Status",
            //    message = "Connected to notification service!"
            //});
        }

        public async Task SendTestNotification()
        {
            await Clients.Caller.SendAsync("ReceiveNotification", new
            {
                type = "info",
                title = "Test Notification",
                message = "This is a test notification."
            });
        }
    }
}
