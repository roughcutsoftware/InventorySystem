using InventorySystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetAllWithCategoryAndSupplier();
        List<Product> GetAllWithCategoryAndSupplier(int size, int pageNumber);
        int GetTotalCount();
        IEnumerable<Product> GetProductsByCategoryId(int categoryId);
    }
}
