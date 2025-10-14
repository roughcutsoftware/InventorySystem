using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Repositories;
using InventorySystem.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace InventorySystem.Infrastructure.Services
{
    public class LogService(ILogRepository logRepo, ILogger<LogService> fileLogger) : ILogService
    {
        private readonly ILogRepository _logRepo = logRepo;
        private readonly ILogger<LogService> _fileLogger = fileLogger;

        public void LogAction(string user, string action, string details)
        {
            var log = new SystemLog
            {
                User = user,
                Action = action,
                Details = details,
                Timestamp = DateTime.Now
            };

            _logRepo.Add(log);
            _logRepo.SaveChanges();

            _fileLogger.LogInformation($"[{DateTime.Now}] {user} - {action} - {details}");
        }
    }
}
