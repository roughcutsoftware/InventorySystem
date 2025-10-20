using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.Repositories;
using InventorySystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

            if (!string.IsNullOrEmpty(includes))
            {
                products = products.Include(includes);
            }

            return products
                .Skip((pageNumber - 1) * size)
                .Take(size)
                .AsNoTracking()
                .ToList();
        }

        public List<Product> GetAllWithCategoryAndSupplier(int size, int pageNumber)
        {
            return context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .OrderBy(p => p.ProductId)
                .Skip((pageNumber - 1) * size)
                .Take(size)
                .AsNoTracking()
                .ToList();
        }

        public List<Product> GetAllWithCategoryAndSupplier()
        {
            return context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .AsNoTracking()
                .ToList();
        }

        public int GetTotalCount()
        {
            return context.Products.Count();
        }

        Product? IRepository<Product>.GetByID(int id, string includes)
        {
            IQueryable<Product> products = context.Products;

            if (!string.IsNullOrEmpty(includes))
            {
                products = products.Include(includes);
            }

            return products.FirstOrDefault(p => p.ProductId == id);
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

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
