using InventorySystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace InventorySystem.Infrastructure.Data.Configurations
{
    public class SalesConfiguration : IEntityTypeConfiguration<Sales>
    {

        public void Configure(EntityTypeBuilder<Sales> builder)
        {
            builder.HasKey(s => s.SaleId);

            builder.Property(s => s.TotalAmount)
                   .HasColumnType("decimal(18,2)");

            builder.Property(s => s.Status)
                   .HasMaxLength(50);

            // Relations

            builder.HasOne(s => s.Customer)
                   .WithMany(c => c.Sales)
                   .HasForeignKey(s => s.CustomerId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(s => s.User)
                   .WithMany(u => u.Sales)
                   .HasForeignKey(s => s.CreatedBy)
                   .OnDelete(DeleteBehavior.NoAction);


            builder.HasData(
           new Sales { SaleId = 1, SaleDate = new DateTime(2024, 1, 10), TotalAmount = 1500.75m, Status = "Completed", CustomerId = 1, CreatedBy = 1 },
           new Sales { SaleId = 2, SaleDate = new DateTime(2024, 2, 5), TotalAmount = 2200.50m, Status = "Pending", CustomerId = 2, CreatedBy = 2 },
           new Sales { SaleId = 3, SaleDate = new DateTime(2024, 3, 15), TotalAmount = 890.00m, Status = "Completed", CustomerId = 3, CreatedBy = 1 },
           new Sales { SaleId = 4, SaleDate = new DateTime(2024, 4, 20), TotalAmount = 4500.99m, Status = "Cancelled", CustomerId = 4, CreatedBy = 3 },
           new Sales{ SaleId = 5, SaleDate = new DateTime(2024, 5, 1), TotalAmount = 3200.10m, Status = "Completed", CustomerId = 5, CreatedBy = 2 }
                    );



        }
        }
    }
