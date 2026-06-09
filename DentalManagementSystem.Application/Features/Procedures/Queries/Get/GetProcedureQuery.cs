using DentalManagementSystem.Application.DTO.Procedures;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Features.Procedures.Queries.Get
{
    public class GetProcedureQuery : IRequest<List<ProcedureDto>>
    {
    }
}
