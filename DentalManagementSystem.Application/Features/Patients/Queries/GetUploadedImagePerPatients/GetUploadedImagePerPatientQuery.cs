using DentalManagementSystem.Application.DTO.Patients;
using DentalManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Features.Patients.Queries.GetUploadedImagePerPatients
{
    public class GetUploadedImagePerPatientQuery : IRequest<List<PatientImageDto>>
    {
        public Guid PatientId { get; set; }

    }
}
