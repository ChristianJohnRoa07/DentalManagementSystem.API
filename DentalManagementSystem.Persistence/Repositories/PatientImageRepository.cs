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
    public class PatientImageRepository : BaseRepository<PatientImage>, IPatientImageRepository
    {
        private readonly DentalManagementSystemDbContext _dbContext;

        public PatientImageRepository(DentalManagementSystemDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PatientImage>> GetImagesByPatientId(Guid patientId)
        {
            return await _dbContext.PatientImages
                .Where(i => i.PatientId == patientId)
                .ToListAsync();
        }
    }
}
