using DentalManagementSystem.Application.Contracts.Persistence;
using DentalManagementSystem.Domain.Entities;
using DentalManagementSystem.Persistence.DatabaseContext;
using DentalManagementSystem.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Persistence.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        private readonly DentalManagementSystemDbContext _dbContext;

        public PatientRepository(DentalManagementSystemDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Patient?> GetPatientWithImages(Guid id)
        {
            return await _dbContext.Patients
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
