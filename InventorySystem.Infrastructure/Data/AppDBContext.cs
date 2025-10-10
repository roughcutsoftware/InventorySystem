using InventorySystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Infrastructure.Data
{
    public class AppDBContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDBContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        #region Tables
        public DbSet<User> Users { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SaleDetails> SaleDetails { get; set; }

        #endregion
    }
}
