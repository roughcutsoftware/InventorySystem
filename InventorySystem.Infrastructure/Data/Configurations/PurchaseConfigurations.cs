using InventorySystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Infrastructure.Data.Configurations
{
    public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase");

            builder.HasKey(p => p.PurchaseId);

            builder.Property(p => p.TotalAmount)
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Status)
                   .HasMaxLength(50);

            builder.HasOne(p => p.Supplier)
                   .WithMany(s => s.Purchases)
                   .HasForeignKey(p => p.SupplierId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.CreatedBy)
                   .WithMany(u => u.Purchases)
                   .HasForeignKey(p => p.CreatedById)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(
               new Purchase { PurchaseId = 1, PurchaseDate = new DateTime(2025,10,10), TotalAmount = 2500.00m, Status = "Completed", SupplierId = 1, CreatedById = "U1" },
               new Purchase { PurchaseId = 2, PurchaseDate = new DateTime(2025, 10, 9), TotalAmount = 1800.50m, Status = "Completed", SupplierId = 2, CreatedById = "U2" },
               new Purchase { PurchaseId = 3, PurchaseDate = new DateTime(2025, 10, 9), TotalAmount = 900.75m, Status = "Pending", SupplierId = 3, CreatedById = "U3" },
               new Purchase { PurchaseId = 4, PurchaseDate = new DateTime(2025, 10, 8), TotalAmount = 3100.00m, Status = "Completed", SupplierId = 1, CreatedById = "U4" },
               new Purchase { PurchaseId = 5, PurchaseDate = new DateTime(2025, 10, 7), TotalAmount = 1200.00m, Status = "Pending", SupplierId = 2, CreatedById = "U5" }
           );
        }
    }
}
