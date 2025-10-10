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
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Product)
                   .WithMany(pr => pr.PurchaseDetails)
                   .HasForeignKey(p => p.ProductId)
                   .OnDelete(DeleteBehavior.NoAction);

            //builder.HasData(
            //       new PurchaseDetails { PurchaseDetailId = 1, Quantity = 10, UnitCost = 100.50m, subTotal = 1005.00m, ProductId = 1, PurchaseId = 1 },
            //       new PurchaseDetails { PurchaseDetailId = 2, Quantity = 5, UnitCost = 200.00m, subTotal = 1000.00m, ProductId = 2, PurchaseId = 1 },
            //       new PurchaseDetails { PurchaseDetailId = 3, Quantity = 20, UnitCost = 50.25m, subTotal = 1005.00m, ProductId = 3, PurchaseId = 2 },
            //       new PurchaseDetails { PurchaseDetailId = 4, Quantity = 15, UnitCost = 75.00m, subTotal = 1125.00m, ProductId = 4, PurchaseId = 3 },
            //       new PurchaseDetails { PurchaseDetailId = 5, Quantity = 8, UnitCost = 120.75m, subTotal = 966.00m, ProductId = 5, PurchaseId = 4 }
            //       );




        }
    }
}

