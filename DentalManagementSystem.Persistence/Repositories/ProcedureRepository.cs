using DentalManagementSystem.Application.Contracts.Persistence;
using DentalManagementSystem.Application.Exceptions;
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
    public class ProcedureRepository : BaseRepository<Procedure>, IProcedureRepository
    {
        private readonly DentalManagementSystemDbContext _dbContext;

        public ProcedureRepository(DentalManagementSystemDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ChangeProcedureStatus(Guid procedureId)
        {
            var procedure = await _dbContext.Procedures.FindAsync(procedureId);

            if (procedure == null)
            {
                throw new NotFoundException($"Procedure with ID {procedureId} was not found.");
            }

            procedure.IsActive = !procedure.IsActive;

            await _dbContext.SaveChangesAsync();
        }
    }
}
