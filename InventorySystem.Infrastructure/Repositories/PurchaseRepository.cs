using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Repositories;
using InventorySystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Infrastructure.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly AppDBContext _context;

        public PurchaseRepository(AppDBContext context) 
        {
            _context = context;
        }
        public void Add(Purchase obj)
        {
            _context.Purchases.Add(obj);
        }

        public void Delete(int id)
        {
            var purchase = _context.Purchases.Find(id);
            if (purchase != null)
            {
                _context.Purchases.Remove(purchase);
            }
        }

        public List<Purchase> GetAll(int size = 20, int pageNumber = 1, string includes = "")
        {
            IQueryable<Purchase> purchases = _context.Purchases;

            if (!String.IsNullOrEmpty(includes))
            {
                purchases = purchases.Include(includes);
            }
            return purchases.Skip((pageNumber - 1) * size).Take(size).AsNoTracking().ToList();
        }

        public Purchase? GetByID(int id, string include = "")
        {
            IQueryable<Purchase> purchases = _context.Purchases;
            if (!String.IsNullOrEmpty(include))
            {
                purchases = purchases.Include(include);
            }
            return purchases.FirstOrDefault(p => p.PurchaseId == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Purchase obj)
        {
            var existingPurchase = _context.Purchases.FirstOrDefault(p => p.PurchaseId == obj.PurchaseId);
            if (existingPurchase != null)
            {
                existingPurchase.PurchaseDate = obj.PurchaseDate;
                existingPurchase.TotalAmount = obj.TotalAmount;
                existingPurchase.Status = obj.Status;
                existingPurchase.SupplierId = obj.SupplierId;
                existingPurchase.CreatedById = obj.CreatedById;
            }
        }
    }
}
