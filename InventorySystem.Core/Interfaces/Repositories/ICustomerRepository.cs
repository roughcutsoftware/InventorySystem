using InventorySystem.Core.Entities;

namespace InventorySystem.Core.Interfaces.Repositories
{
    public interface ICustomerRepository :IRepository<Customer>
    {
        int GetTotalCount();
    }
}
