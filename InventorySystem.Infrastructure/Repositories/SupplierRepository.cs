using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Repositories;
using InventorySystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            throw new NotImplementedException();
        }

        void IRepository<Supplier>.Delete(int id)
        {
            throw new NotImplementedException();
        }

        List<Supplier> IRepository<Supplier>.GetAll(int size, int pageNumber, string includes)
        {
            throw new NotImplementedException();
        }

        Supplier? IRepository<Supplier>.GetByID(int id, string include)
        {
            throw new NotImplementedException();
        }

        void IRepository<Supplier>.SaveChanges()
        {
            throw new NotImplementedException();
        }

        void IRepository<Supplier>.Update(Supplier obj)
        {
            throw new NotImplementedException();
        }
    }
}
