using Microsoft.AspNetCore.SignalR;

namespace InventorySystem.Core.Hubs
{
    public class NotificationHub:Hub
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await Clients.Caller.SendAsync("ReceiveNotification", "Connected to notification service!");
        }
    }
}
