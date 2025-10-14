using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Repositories;

namespace InventorySystem.Core.Interfaces.Services
{
    public interface ILogService
    {
        void LogAction(string user, string action, string details);
    }
}
