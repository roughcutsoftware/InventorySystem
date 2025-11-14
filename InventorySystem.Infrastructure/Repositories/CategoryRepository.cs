using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorySystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace InventorySystem.Infrastructure.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly AppDBContext _context;

        public CategoryRepository(AppDBContext context)
        {
            _context = context;
        }

        public void Add(Category obj)
        {
            _context.Categories.Add(obj);
        }

        public void Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
        }

        public int GetTotalCount()
        {
            return _context.Categories.Count();
        }

        List<Category> IRepository<Category>.GetAll(int size, int pageNumber, string includes)
        {
            IQueryable<Category>categories = _context.Categories;
            if (!String.IsNullOrEmpty(includes))
            {
                categories = categories.Include(includes);  
            }
            return categories.Skip((pageNumber - 1) * size)
                .Take(size)
                .AsNoTracking()
                .ToList();

        }

         Category? IRepository<Category>.GetByID(int id, string include )
        {
            IQueryable<Category> categories = _context.Categories;

            if (!String.IsNullOrEmpty(include))
            {
                categories = categories.Include(include);
            }

            return categories.FirstOrDefault(c => c.CategoryId == id);
        }



        void IRepository<Category>.SaveChanges()
        {
            _context.SaveChanges();
        }

        void IRepository<Category>.Update(Category obj)
        {
            var existingCategory = _context.Categories
                .FirstOrDefault(s => s.CategoryId == obj.CategoryId);

            if (existingCategory != null)
            {
                existingCategory.Name = obj.Name;
                existingCategory.Description = obj.Description;
                
            }
        }
    }

}

