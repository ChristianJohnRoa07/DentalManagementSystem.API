using DentalManagementSystem.Application.DTO.Procedures;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Features.Procedures.Queries.GetById
{
    public class GetProcedureByIdQuery : IRequest<ProcedureDto>
    {
        public Guid Id { get; set; }
    }
}
