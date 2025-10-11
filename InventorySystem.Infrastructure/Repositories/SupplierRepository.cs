using AutoMapper;
using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Repositories;
using InventorySystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Infrastructure.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly AppDBContext _context;
        public SupplierRepository(AppDBContext context)
        {
            _context = context;
        }

        void IRepository<Supplier>.Add(Supplier obj)
        {
            _context.Suppliers.Add(obj);
        }

        void IRepository<Supplier>.Delete(int id)
        {
            var supplier = _context.Suppliers.Find(id);

            if(supplier != null)
            {
                _context.Suppliers.Remove(supplier);
            }
        }

        List<Supplier> IRepository<Supplier>.GetAll(int size, int pageNumber, string includes)
        {
            IQueryable<Supplier> suppliers = _context.Suppliers;
            if (!String.IsNullOrEmpty(includes))
            {
                suppliers = suppliers.Include(includes);
            }
            return suppliers.Skip((pageNumber - 1) * size)
                .Take(size)
                .AsNoTracking()
                .ToList();
        }

        Supplier? IRepository<Supplier>.GetByID(int id, string include)
        {
            IQueryable<Supplier> supplier = _context.Suppliers;

            if (!String.IsNullOrEmpty(include))
            {
                supplier = supplier.Include(include);
            }

            return supplier.FirstOrDefault(s=>s.SupplierId == id);
        }

        void IRepository<Supplier>.SaveChanges()
        {
            _context.SaveChanges();
        }

        void IRepository<Supplier>.Update(Supplier obj)
        {
            var existingSupplier = _context.Suppliers.FirstOrDefault(s => s.SupplierId == obj.SupplierId);

            if (existingSupplier != null)
            {
                existingSupplier.Name = obj.Name;
                existingSupplier.ContactName = obj.ContactName;
                existingSupplier.CompanyName = obj.CompanyName;
                existingSupplier.Email = obj.Email;
                existingSupplier.Phone = obj.Phone;
                existingSupplier.Address = obj.Address;
                existingSupplier.IsActive = obj.IsActive;
            }
        }
    }
}
