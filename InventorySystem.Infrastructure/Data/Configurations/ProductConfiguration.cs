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


        }
    }

}