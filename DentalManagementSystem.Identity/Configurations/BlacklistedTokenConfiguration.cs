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
    public class BlacklistedTokenConfiguration : IEntityTypeConfiguration<BlacklistedToken>
    {
        public void Configure(EntityTypeBuilder<BlacklistedToken> builder)
        {
            builder.HasKey(q => q.Id);

            builder.Property(q => q.Token)
               .IsRequired()
               .HasColumnType("nvarchar(max)");

            builder.Property(q => q.TokenHash)
                   .IsRequired()
                   .HasMaxLength(64);

            builder.HasIndex(q => q.TokenHash)
                   .IsUnique();
        }
    }
}
