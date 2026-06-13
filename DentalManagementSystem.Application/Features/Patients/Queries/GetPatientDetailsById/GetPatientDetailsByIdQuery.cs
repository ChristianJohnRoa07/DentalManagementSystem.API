using DentalManagementSystem.Application.DTO.Patients;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Features.Patients.Queries.GetPatientDetailsById
{
    public class GetPatientDetailsByIdQuery : IRequest<PatientDto>
    {
        public Guid Id { get; set; }
    }
}
