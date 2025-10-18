using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Repositories;
using InventorySystem.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InventorySystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LogsController : Controller
    {
        private readonly ILogService _logService;
        private readonly ILogRepository _logRepository;

        public LogsController(ILogService logService, ILogRepository logRepository)
        {
            _logService = logService;
            _logRepository = logRepository;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            try
            {
                var logs = _logRepository.GetAll(pageSize, page)
                    .OrderByDescending(l => l.Timestamp)
                    .ToList();

                // Calculate total pages
                var totalLogs = _logRepository.GetAll().Count;
                var totalPages = (int)Math.Ceiling(totalLogs / (double)pageSize);

                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
                ViewBag.PageSize = pageSize;

                return View(logs);
            }
            catch (Exception ex)
            {
                _logService.LogAction(User.Identity?.Name ?? "System", "View Logs Error", ex.Message);
                TempData["ErrorMessage"] = "Error retrieving logs.";
                return View(new List<SystemLog>());
            }
        }

        [HttpPost]
        public IActionResult Clear()
        {
            try
            {
                var logs = _logRepository.GetAll();
                foreach (var log in logs)
                {
                    _logRepository.Delete(log.Id);
                }
                _logRepository.SaveChanges();

                _logService.LogAction(User.Identity?.Name ?? "System", "Clear Logs", "All system logs cleared");
                TempData["SuccessMessage"] = "Logs cleared successfully.";
            }
            catch (Exception ex)
            {
                _logService.LogAction(User.Identity?.Name ?? "System", "Clear Logs Error", ex.Message);
                TempData["ErrorMessage"] = "Error clearing logs.";
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Export()
        {
            try
            {
                var logs = _logRepository.GetAll().OrderByDescending(l => l.Timestamp);
                var csv = new System.Text.StringBuilder();
                
                // Add CSV header
                csv.AppendLine("Timestamp,User,Action,Details");

                // Add log entries
                foreach (var log in logs)
                {
                    csv.AppendLine($"{log.Timestamp:yyyy-MM-dd HH:mm:ss},{log.User},{log.Action},{log.Details}");
                }

                _logService.LogAction(User.Identity?.Name ?? "System", "Export Logs", "Logs exported to CSV");
                return File(System.Text.Encoding.UTF8.GetBytes(csv.ToString()), 
                    "text/csv", 
                    $"system_logs_{DateTime.Now:yyyyMMdd_HHmmss}.csv");
            }
            catch (Exception ex)
            {
                _logService.LogAction(User.Identity?.Name ?? "System", "Export Logs Error", ex.Message);
                TempData["ErrorMessage"] = "Error exporting logs.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}