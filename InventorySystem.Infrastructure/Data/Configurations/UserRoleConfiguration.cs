using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Infrastructure.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            //builder.HasData(
            //    // Admin users (U1, U2)
            //    new IdentityUserRole<string> { UserId = "U1", RoleId = "R1" },
            //    new IdentityUserRole<string> { UserId = "U2", RoleId = "R1" },

            //    // Normal users (U3, U4, U5)
            //    new IdentityUserRole<string> { UserId = "U3", RoleId = "R2" },
            //    new IdentityUserRole<string> { UserId = "U4", RoleId = "R2" },
            //    new IdentityUserRole<string> { UserId = "U5", RoleId = "R2" }
            //);
        }
    }
}
