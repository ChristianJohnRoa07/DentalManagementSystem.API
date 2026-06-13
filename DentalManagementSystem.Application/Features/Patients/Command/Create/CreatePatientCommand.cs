using DentalManagementSystem.Application.DTO.Patients;
using DentalManagementSystem.Application.DTO.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Features.Patients.Command.Create
{
    public class CreatePatientCommand : IRequest<BaseCommandResponse>
    {
        public CreatePatientDto createPatientDto { get; set; }
    }
}
