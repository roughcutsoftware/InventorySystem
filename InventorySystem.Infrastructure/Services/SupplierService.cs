using InventorySystem.Core.DTOs;
using InventorySystem.Core.Interfaces;

namespace InventorySystem.Infrastructure.Services
{
    public class SupplierService : ISupplierService
    {
        public Task AddSupplierAsync(SupplierDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSupplierAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SupplierDto?> GetSupplierByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSupplierAsync(SupplierDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
