using AutoMapper;
using DentalManagementSystem.Application.Contracts.Persistence;
using DentalManagementSystem.Application.DTO.Procedures;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Features.Procedures.Queries.Get
{
    public class GetProcedureHandler : IRequestHandler<GetProcedureQuery, List<ProcedureDto>>
    {
        private readonly IProcedureRepository _procedureRepository;
        private readonly IMapper _mapper;

        public GetProcedureHandler(IProcedureRepository procedureRepository, IMapper mapper)
        {
            _procedureRepository = procedureRepository;
            _mapper = mapper;
        }

        public async Task<List<ProcedureDto>> Handle(GetProcedureQuery request, CancellationToken cancellationToken)
        {
            var procedures = await _procedureRepository.GetAll();

            return _mapper.Map<List<ProcedureDto>>(procedures);
        }
    }
}
