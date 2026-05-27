using AutoMapper;
using DentalManagementSystem.Application.Contracts.Persistence;
using DentalManagementSystem.Application.DTO.Appointment;
using DentalManagementSystem.Application.DTO.Responses;
using DentalManagementSystem.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Features.Appointment.Queries.GetAllAppointments
{
    public class GetAllAppointmentsHandler : IRequestHandler<GetAllAppointmentsCommand, List<AppointmentDTO>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public GetAllAppointmentsHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<List<AppointmentDTO>> Handle(GetAllAppointmentsCommand request, CancellationToken cancellationToken)
        {
            var appointments = await _appointmentRepository.GetAll();

            var appointmentsData = _mapper.Map<List<AppointmentDTO>>(appointments);

            if (appointmentsData == null || !appointmentsData.Any()) 
            {
                throw new NotFoundException("No appointments found.");
            }

            return appointmentsData;
        }
    }
}
