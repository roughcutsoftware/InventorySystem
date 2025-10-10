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

            builder.Property(s => s.Name).HasMaxLength(50).IsRequired();
            builder.Property(s => s.ContactName).HasMaxLength(50);
            builder.Property(s => s.CompanyName).HasMaxLength(50);
            builder.Property(s => s.Email).HasMaxLength(50);
            //builder.Property(s => s.Phone).HasMaxLength(50);
            builder.Property(s => s.Address).HasMaxLength(50);

            builder.HasData(
             new Supplier { SupplierId = 1, Name = "Ahmed Hassan", ContactName = "Ahmed", CompanyName = "Tech Supplies", Email = "ahmed@techsupplies.com", Phone = 01001234567, Address = "Cairo", IsActive = true },
             new Supplier { SupplierId = 2, Name = "Sara Ali", ContactName = "Sara", CompanyName = "Alpha Traders", Email = "sara@alphatraders.com", Phone = 01007654321, Address = "Giza", IsActive = true },
             new Supplier { SupplierId = 3, Name = "Mohamed Samir", ContactName = "Mohamed", CompanyName = "SmartParts", Email = "mohamed@smartparts.com", Phone = 01123456789, Address = "Alexandria", IsActive = true },
             new Supplier { SupplierId = 4, Name = "Nour Khaled", ContactName = "Nour", CompanyName = "ProLink", Email = "nour@prolink.com", Phone = 01009876543, Address = "Mansoura", IsActive = false },
             new Supplier { SupplierId = 5, Name = "Omar Fathy", ContactName = "Omar", CompanyName = "MegaTech", Email = "omar@megatech.com", Phone = 01234567890, Address = "Tanta", IsActive = true }
         );


        }

    }
}
