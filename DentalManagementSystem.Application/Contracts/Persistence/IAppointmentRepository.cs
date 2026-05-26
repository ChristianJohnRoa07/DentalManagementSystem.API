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
        Task<Appointment> GetAppointment(Guid Id);
        Task<List<Appointment>> GetAppointmentsByPatient(Guid PatientId);
        Task<List<Appointment>> GetAppointmentsByProcedure(Guid ProcedureId);
        Task<List<Appointment>> GetAppointmentsByDate(DateTime Date);

    }
}
