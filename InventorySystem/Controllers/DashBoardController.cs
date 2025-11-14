using InventorySystem.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.web.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize]
    public class DashBoardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        public DashBoardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;

        }
        [HttpGet]
        public IActionResult Index()
        {
            var startDate = DateTime.Today.AddDays(-30);
            var endDate = DateTime.Today;
            var summary = _dashboardService.GetDashboardSummary(startDate, endDate);
            return View(summary);
        }

        [HttpPost]
        public IActionResult Index(DateTime? from , DateTime? to)
        {
            // If user didn't specify a date range, default to last 30 days

            var startDate = from ?? DateTime.Today.AddDays(-30);
            var endDate = to ?? DateTime.Today;

            var summary = _dashboardService.GetDashboardSummary(startDate, endDate);

            return View(summary);
        }
    }
}
