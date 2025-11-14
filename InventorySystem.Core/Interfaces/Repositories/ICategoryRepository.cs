using InventorySystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        //    IEnumerable<Category> GetAll();
        //    Category GetById(int id);
        //    void Add(Category category);
        //    void Update(Category category);
        //    void Delete(Category category);
        //    void Save();
        //

        int GetTotalCount();
    }
}
