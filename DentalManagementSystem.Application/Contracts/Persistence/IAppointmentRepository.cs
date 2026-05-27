using DentalManagementSystem.Application.Contracts.Persistence.Common;
using DentalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Contracts.Persistence
{
    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {
        Task<List<Appointment>> GetAppointmentsByPatient(Guid patientId);
        Task<List<Appointment>> GetAppointmentsByProcedure(Guid procedureId);
        Task<List<Appointment>> GetAppointmentsByDate(DateTime date);

    }
}
