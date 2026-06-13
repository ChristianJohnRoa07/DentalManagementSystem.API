using DentalManagementSystem.Application.Contracts.Identity;
using DentalManagementSystem.Domain.Entities;
using DentalManagementSystem.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Persistence.DatabaseContext
{
    public class DentalManagementSystemDbContext : DbContext
    {
        private readonly IUserService _userService;

        public DentalManagementSystemDbContext(DbContextOptions<DentalManagementSystemDbContext> options, IUserService userService) : base(options)
        {
            _userService = userService;
        }

        public DentalManagementSystemDbContext(DbContextOptions<DentalManagementSystemDbContext> options)
        : base(options)
        {
            // Assign a fallback structure or an inline implementation so _userService is never null
            _userService = new FallbackUserService();
        }

        private class FallbackUserService : IUserService
        {
            public Guid? UserId => Guid.Empty;
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientImage> PatientImages { get; set; }
        public DbSet<Procedure> Procedures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DentalManagementSystemDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entry in ChangeTracker.Entries<BaseDomainEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.LastModifiedDate = DateTime.Now;
                entry.Entity.LastModifiedBy = _userService.UserId;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                    entry.Entity.CreatedBy = _userService.UserId;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
