using InventorySystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Infrastructure.Data.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(s => s.SupplierId);

            builder.Property(s => s.Name)
                .HasMaxLength(50)
                .IsRequired();
                
            builder.Property(s => s.ContactName)
                .HasMaxLength(50);
                
            builder.Property(s => s.CompanyName)
                .HasMaxLength(50);
                
            builder.Property(s => s.Email)
                .HasMaxLength(50);
                
            builder.Property(s => s.Address)
                .HasMaxLength(50);
        }
    }
}
