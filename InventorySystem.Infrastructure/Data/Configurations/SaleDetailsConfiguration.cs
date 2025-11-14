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
               .OnDelete(DeleteBehavior.SetNull);

           builder.HasOne(s=>s.Product)
                .WithMany(p=>p.SaleDetails)
                .HasForeignKey(f=>f.ProductId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
