using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Repositories;
using InventorySystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Infrastructure.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        private readonly AppDBContext _context;
        public SalesRepository(AppDBContext context)
        {
            _context = context;
        }
        public void Add(Sales obj)
        {
            _context.Sales.Add(obj);
        }

        public void Delete(int id)
        {
            var sale = _context.Sales.Find(id);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
            }

        }

        public List<Sales> GetAll(int size = 20, int pageNumber = 1, string includes = "")
        {
            IQueryable<Sales> sales = _context.Sales;
            if (!string.IsNullOrEmpty(includes))
            {
                sales = sales.Include(includes);
            }

            return _context.Sales
                .Include(s => s.Customer)
                .Skip((pageNumber - 1) * size)
                .Take(size)
                .ToList();
        }
        

        public Sales? GetByID(int id, string include = "")
        {
            IQueryable<Sales> sales = _context.Sales;

            if (!string.IsNullOrEmpty(include))
                sales = sales.Include(include);

            return sales
                .Include(s => s.Customer)
                .Include(s => s.SaleDetails)
                .ThenInclude(d => d.Product)
                .FirstOrDefault(s => s.SaleId == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Sales obj)
        {
            var existingSales = _context.Sales.FirstOrDefault(s => s.SaleId == obj.SaleId);
            if (existingSales != null)
            {
                existingSales.SaleDetails = obj.SaleDetails;
                existingSales.TotalAmount = obj.TotalAmount;
                existingSales.Status = obj.Status;
                existingSales.CustomerId = obj.CustomerId;
                existingSales.CreatedBy = obj.CreatedBy;
            }
        }


    }
}
