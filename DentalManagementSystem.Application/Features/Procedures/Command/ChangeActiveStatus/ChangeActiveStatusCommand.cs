using DentalManagementSystem.Application.DTO.Procedure;
using DentalManagementSystem.Application.DTO.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Features.Procedures.Command.ChangeActiveStatus
{
    public class ChangeActiveStatusCommand : IRequest<BaseCommandResponse>
    {
        public ChangeActiveStatusDto changeActiveStatusDto { get; set; }
    }
}
