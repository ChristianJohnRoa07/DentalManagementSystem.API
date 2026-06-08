using DentalManagementSystem.Application.DTO.Procedures;
using DentalManagementSystem.Application.DTO.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Features.Procedures.Command.Update
{
    public class UpdateProcedureCommand : IRequest<BaseCommandResponse>
    {
        public UpdateProcedureDto updateProcedureDto { get; set; }
    }
}
