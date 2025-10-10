using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorySystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace InventorySystem.Infrastructure.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.CategoryId);

            builder.Property(c => c.Name)
                  .IsRequired()
                  .HasMaxLength(50);

            builder.Property(c => c.Description)
                  .HasMaxLength(150);
            //builder.HasData(
            //           new Category { CategoryId = 1, Name = "Electronics", Description = "Devices and gadgets" },
            //           new Category { CategoryId = 2, Name = "Clothing", Description = "Men and women clothes" },
            //           new Category { CategoryId = 3, Name = "Groceries", Description = "Food and beverages" },
            //           new Category { CategoryId = 4, Name = "Furniture", Description = "Home and office furniture" },
            //           new Category { CategoryId = 5, Name = "Sports", Description = "Sportswear and equipment" }
            //       );

            //builder.HasMany(c => c.Products)
            //      .WithOne(p => p.Category)
            //      .HasForeignKey(p => p.CategoryId)
            //      .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
