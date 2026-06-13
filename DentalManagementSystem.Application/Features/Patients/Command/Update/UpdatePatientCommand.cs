using DentalManagementSystem.Application.DTO.Patients;
using DentalManagementSystem.Application.DTO.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Features.Patients.Command.Update
{
    public class UpdatePatientCommand : IRequest<BaseCommandResponse>
    {
        public UpdatePatientDto updatePatientDto { get; set; }
    }
}
