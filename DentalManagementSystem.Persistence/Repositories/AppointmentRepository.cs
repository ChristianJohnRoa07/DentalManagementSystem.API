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
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        private readonly DentalManagementSystemDbContext _dbContext;

        public AppointmentRepository(DentalManagementSystemDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<List<Appointment>> GetAppointmentsByDate(DateTime date)
        {
            var targetDate = date.Date;

            return await _dbContext.Appointments.Where(data => data.AppointmentDateTime.Date == targetDate).ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByPatient(Guid patientId)
        {
            return await _dbContext.Appointments.Where(data => data.Patient.Id == patientId).ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByProcedure(Guid procedureId)
        {
            return await _dbContext.Appointments.Where(data => data.Procedure.Id == procedureId).ToListAsync();
        }
    }
}
