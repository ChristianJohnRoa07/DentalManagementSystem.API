using DentalManagementSystem.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(
                new ApplicationUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "ADMIN@LOCALHOST.COM",
                    FirstName = "System",
                    LastName = "Admin",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    PasswordHash = "AQAAAAIAAYagAAAAEJa6+guO7sX0smJaX+9AZRZ4X28hUtrR3E0dZU8tUXTrwChsY6E40wL1EgYSDoK19Q==", //P@ssw0rd!
                    EmailConfirmed = true,
                    SecurityStamp = string.Empty,
                    ConcurrencyStamp = string.Empty,
                },
                new ApplicationUser
                {
                    Id = "9e224968-33e4-4652-b7b7-8574d048cdb9",
                    Email = "staff1@localhost.com",
                    NormalizedEmail = "STAFF1@LOCALHOST.COM",
                    FirstName = "System",
                    LastName = "Staff1",
                    UserName = "staff1",
                    NormalizedUserName = "STAFF1",
                    PasswordHash = "AQAAAAIAAYagAAAAEJa6+guO7sX0smJaX+9AZRZ4X28hUtrR3E0dZU8tUXTrwChsY6E40wL1EgYSDoK19Q==", //P@ssw0rd!
                    EmailConfirmed = true,
                    SecurityStamp = string.Empty,
                    ConcurrencyStamp = string.Empty,
                }
            );
        }
    }
}
