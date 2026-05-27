using DentalManagementSystem.Application.Contracts.Persistence;
using DentalManagementSystem.Domain.Entities;
using DentalManagementSystem.Persistence.DatabaseContext;
using DentalManagementSystem.Persistence.Repositories.Common;
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


    }
}
