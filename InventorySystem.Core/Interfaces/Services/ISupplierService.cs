using InventorySystem.Core.DTOs;

namespace InventorySystem.Core.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync();
        Task<SupplierDto?> GetSupplierByIdAsync(int id);
        Task AddSupplierAsync(SupplierDto dto);
        Task UpdateSupplierAsync(SupplierDto dto);
        Task DeleteSupplierAsync(int id);
    }
}