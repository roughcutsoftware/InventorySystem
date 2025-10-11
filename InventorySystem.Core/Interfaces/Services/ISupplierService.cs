using InventorySystem.Core.DTOs;

namespace InventorySystem.Core.Interfaces
{
    public interface ISupplierService
    {
        List<SupplierDto> GetAllSuppliers(int size = 20, int pageNumber = 1);
        SupplierDto? GetSupplierById(int id);
        void AddSupplier(SupplierDto dto);
        void UpdateSupplier(SupplierDto dto);
        void DeleteSupplier(int id);
    }
}