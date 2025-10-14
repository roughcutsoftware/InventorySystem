using InventorySystem.Core.DTOs;

namespace InventorySystem.Core.Interfaces.Services
{
    public interface IDashboardService
    {
        DashboardSummaryDto GetDashboardSummary(DateTime from, DateTime to);
    }
}
