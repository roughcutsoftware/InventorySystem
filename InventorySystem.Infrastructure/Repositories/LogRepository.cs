using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Repositories;
using InventorySystem.Infrastructure.Data;

namespace InventorySystem.Infrastructure.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly AppDBContext _context;

        public LogRepository(AppDBContext context)
        {
            _context = context;
        }

        public void Add(SystemLog log)
        {
            _context.SystemLogs.Add(log);
        }

        public void Delete(int id)
        {
            var log = _context.SystemLogs.Find(id);
            if (log != null)
                _context.SystemLogs.Remove(log);
        }

        public List<SystemLog> GetAll(int size = 20, int pageNumber = 1, string includes = "")
        {
            return _context.SystemLogs
                .OrderByDescending(l => l.Timestamp)
                .Skip((pageNumber - 1) * size)
                .Take(size)
                .ToList();
        }

        public SystemLog? GetByID(int id, string include = "")
        {
            return _context.SystemLogs.FirstOrDefault(l => l.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(SystemLog obj)
        {
            _context.SystemLogs.Update(obj);
        }
    }
}
