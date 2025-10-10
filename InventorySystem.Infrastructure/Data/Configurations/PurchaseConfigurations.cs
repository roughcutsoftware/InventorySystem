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

            //builder.HasOne(p => p.Supplier)
            //       .WithMany(s => s.Purchases)
            //       .HasForeignKey(p => p.SupplierId)
            //       .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.User)
                   .WithMany(u => u.Purchases)
                   .HasForeignKey(p => p.CreatedBy)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
