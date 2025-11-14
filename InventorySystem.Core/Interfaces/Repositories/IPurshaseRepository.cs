using InventorySystem.Core.Entities;

namespace InventorySystem.Core.Interfaces.Repositories
{
    public interface IPurchaseRepository:IRepository<Purchase>
    {
        int GetTotalCount();
    }
}
