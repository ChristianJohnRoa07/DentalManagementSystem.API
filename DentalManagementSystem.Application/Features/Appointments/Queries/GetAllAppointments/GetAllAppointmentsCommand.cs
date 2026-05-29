using DentalManagementSystem.Application.DTO.Appointments;
using DentalManagementSystem.Application.DTO.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Features.Appointments.Queries.GetAllAppointments
{
    public class GetAllAppointmentsCommand : IRequest<List<AppointmentDTO>>
    {
    }
}
