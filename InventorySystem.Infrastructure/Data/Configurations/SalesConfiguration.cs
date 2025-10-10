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
        }
        }
    }
