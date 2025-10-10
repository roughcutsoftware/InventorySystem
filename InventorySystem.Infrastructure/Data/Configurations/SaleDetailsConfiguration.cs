using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InventorySystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Infrastructure.Data.Configurations
{
    public class SaleDetailsConfiguration : IEntityTypeConfiguration<SaleDetails>
    {
        public void Configure(EntityTypeBuilder<SaleDetails> builder)
        {
            builder.HasKey(sd => sd.SaleDetailId);

            builder.Property(sd => sd.Quantity)
                  .IsRequired();

            builder.HasOne(s => s.Sales)
               .WithMany(p => p.SaleDetails)
               .HasForeignKey(f => f.SaleId)
               .OnDelete(DeleteBehavior.NoAction);

           builder.HasOne(s=>s.Product)
                .WithMany(p=>p.SaleDetails)
                .HasForeignKey(f=>f.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

        //    builder.HasData(
        //    new SaleDetails
        //    {
        //        SaleDetailId=1,
        //        ProductId = 1,
        //        SaleId = 1,
                
        //        Quantity = 2,
        //        UnitPrice = 12000,
        //        Subtotal = 24000
        //    },
        //    new SaleDetails
        //    {
        //        SaleDetailId = 2,
        //        ProductId = 2,
        //        SaleId = 2,
        //        Quantity = 1,
        //        UnitPrice = 25000,
        //        Subtotal = 25000
        //    },
        //    new SaleDetails
        //    {
        //        SaleDetailId = 3,
        //        ProductId = 3,
        //        SaleId = 3,
        //        Quantity = 5,
        //        UnitPrice = 300,
        //        Subtotal = 1500
        //    },
        //    new SaleDetails
        //    {
        //        SaleDetailId = 4,
        //        ProductId = 4,
        //        SaleId = 4,
        //        Quantity = 10,
        //        UnitPrice = 50,
        //        Subtotal = 500
        //    },
        //    new SaleDetails
        //    {
        //        SaleDetailId = 5,
        //        ProductId = 5,     
        //        Quantity = 1,
        //        UnitPrice = 1000,
        //        Subtotal = 1000
        //    }
        //);
        }
    }
}
