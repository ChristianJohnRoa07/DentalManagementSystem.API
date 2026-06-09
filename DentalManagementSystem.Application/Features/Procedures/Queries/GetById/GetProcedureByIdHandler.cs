using AutoMapper;
using DentalManagementSystem.Application.Contracts.Persistence;
using DentalManagementSystem.Application.DTO.Procedures;
using DentalManagementSystem.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Features.Procedures.Queries.GetById
{
    public class GetProcedureByIdHandler : IRequestHandler<GetProcedureByIdQuery, ProcedureDto>
    {
        private readonly IProcedureRepository _procedureRepository;
        private readonly IMapper _mapper;

        public GetProcedureByIdHandler(IProcedureRepository procedureRepository, IMapper mapper)
        {
            _procedureRepository = procedureRepository;
            _mapper = mapper;
        }

        public async Task<ProcedureDto> Handle(GetProcedureByIdQuery request, CancellationToken cancellationToken)
        {
            var procedureId = request.Id;

            var procedure = await _procedureRepository.GetById(procedureId);

            if (procedure == null) 
            {
                throw NotFoundException.ProcedureNotFound(procedureId);
            }

            return _mapper.Map<ProcedureDto>(procedure);
        }
    }
}
