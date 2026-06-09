using DentalManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Persistence.Configurations.Entities
{
    public class PatientImageConfiguration : IEntityTypeConfiguration<PatientImage>
    {
        public void Configure(EntityTypeBuilder<PatientImage> builder)
        {
            builder.Property(pi => pi.FilePath)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(pi => pi.FileName)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasOne(pi => pi.Patient)
                .WithMany(p => p.Images)
                .HasForeignKey(pi => pi.PatientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
