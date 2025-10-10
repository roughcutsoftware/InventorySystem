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
    public class PurchaseDetailsConfiguration :IEntityTypeConfiguration<PurchaseDetails>
    {
        public void Configure(EntityTypeBuilder<PurchaseDetails> builder)
        {
            builder.HasKey(p => p.PurchaseDetailId);

            builder.Property(p => p.UnitCost)
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.subTotal)
                   .HasColumnType("decimal(18,2)");

            // Relations
            builder.HasOne(p => p.Purchase)
                   .WithMany(pr => pr.PurchaseDetails)
                   .HasForeignKey(p => p.PurchaseId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Product)
                   .WithMany(pr => pr.PurchaseDetails)
                   .HasForeignKey(p => p.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

