using InventorySystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductId);

            builder.Property(p => p.Name)
                  .IsRequired()
                  .HasMaxLength(50);

            
            builder.Property(p => p.QuantityInStock)
                  .IsRequired();


            //builder.HasMany(p => p.SaleDetails)
            //      .WithOne(sd => sd.Product)
            //      .HasForeignKey(sd => sd.ProductId)
            //      .OnDelete(DeleteBehavior.NoAction);

           

            builder.HasOne(p=>p.Category)
                .WithMany(p=>p.Products)
                .HasForeignKey(p=>p.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p=>p.Supplier)
                .WithMany(p=>p.Products)
                .HasForeignKey(p=>p.SupplierId)
                .OnDelete(DeleteBehavior.NoAction);

        //    builder.HasData(
        //    new Product
        //    {
        //        ProductId= 1,
                
        //        SupplierId = 1,
        //        Name = "Smartphone",
        //        CategoryId = 1,
        //        UnitPrice = 12000,
        //        CostPrice = 9000,
        //        QuantityInStock = 50,
        //        ReorderLevel = 10,
        //        IsActive = true,
        //        CreatedAt = new DateTime(2025, 10, 10)
        //    },
        //    new Product
        //    {
        //        ProductId = 2,
        //        SupplierId = 1,
        //        Name = "Laptop",
        //        CategoryId = 1,
        //        UnitPrice = 25000,
        //        CostPrice = 20000,
        //        QuantityInStock = 30,
        //        ReorderLevel = 5,
        //        IsActive = true,
        //        CreatedAt = new DateTime(2025, 10, 10)
        //    },
        //    new Product
        //    {
        //        ProductId = 3,
        //        SupplierId = 2,
        //        Name = "T-shirt",
        //        CategoryId = 2,
        //        UnitPrice = 300,
        //        CostPrice = 150,
        //        QuantityInStock = 100,
        //        ReorderLevel = 15,
        //        IsActive = true,
        //        CreatedAt = new DateTime(2025, 10, 10)
        //    },
        //    new Product
        //    {
        //        ProductId = 4,
        //        SupplierId = 3,
        //        Name = "Apple Juice",
        //        CategoryId = 3,
        //        UnitPrice = 50,
        //        CostPrice = 25,
        //        QuantityInStock = 200,
        //        ReorderLevel = 20,
        //        IsActive = true,
        //        CreatedAt = new DateTime(2025, 10, 10)
        //    },
        //    new Product
        //    {
        //        ProductId = 5,
        //        SupplierId = 4,
        //        Name = "chair",
        //        CategoryId = 4,
        //        UnitPrice = 1000,
        //        CostPrice = 100,
        //        QuantityInStock = 15,
        //        ReorderLevel = 10,
        //        IsActive = true,
        //        CreatedAt = new DateTime(2025, 10, 10)
        //    }
        //);


        }
    }

}