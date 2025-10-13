using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Repositories;
using InventorySystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDBContext context;

        public ProductRepository(AppDBContext _context)
        {
            context = _context;
        }

        public void Add(Product obj)
        {
            context.Add(obj);
        }

        public void Delete(int id)
        {
            var prd = context.Products.Find(id);
            if (prd != null)
            {
                context.Products.Remove(prd);
            }
        }

        List<Product> IRepository<Product>.GetAll(int size, int pageNumber, string includes)
        {
            IQueryable<Product> products = context.Products;

            if (!String.IsNullOrEmpty(includes))
            {
                products = products.Include(includes);
            }
            return products.Skip((pageNumber - 1) * size)
                .Take(size)
                .AsNoTracking()
                .ToList();
        }

        Product? IRepository<Product>.GetByID(int id, string includes)
        {
            IQueryable<Product> products = context.Products;

            if (!String.IsNullOrEmpty(includes))
            {
                products = products.Include(includes);
            }
            return products.FirstOrDefault(p => p.ProductId == id);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        void IRepository<Product>.Update(Product obj)
        {
            var prd = context.Products.FirstOrDefault(p => p.ProductId == obj.ProductId);

            if (prd != null)
            {
                prd.Name = obj.Name;
                prd.QuantityInStock = obj.QuantityInStock;
                prd.UnitPrice = obj.UnitPrice;
                prd.CostPrice = obj.CostPrice;
                prd.ReorderLevel = obj.ReorderLevel;
                prd.IsActive = obj.IsActive;
                prd.CreatedAt = obj.CreatedAt;
                prd.CategoryId = obj.CategoryId;
                prd.SupplierId = obj.SupplierId;
            }
        }
    }
}
