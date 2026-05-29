using AutoMapper;
using DentalManagementSystem.Domain.Entities;
using DentalManagementSystem.Application.Contracts.Persistence;
using DentalManagementSystem.Application.DTO.Procedures.Validator;
using DentalManagementSystem.Application.DTO.Responses;
using DentalManagementSystem.Application.Exceptions;

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Features.Procedures.Command.Create
{
    public class CreateProcedureHandler : IRequestHandler<CreateProcedureCommand, BaseCommandResponse>
    {
        private readonly IProcedureRepository _procedureRepository;
        private readonly IMapper _mapper;

        public CreateProcedureHandler(IProcedureRepository procedureRepository, IMapper mapper) 
        {
            _procedureRepository = procedureRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateProcedureCommand request, CancellationToken cancellationToken)
        {
            var createProcedureRequest = request.createProcedureDto;
            var response = new BaseCommandResponse();
            var validator = new CreateProcedureValidator();
            var validatorResult = await validator.ValidateAsync(createProcedureRequest, cancellationToken);

            if (!validatorResult.IsValid)
            {
                throw new BadRequestException("Validation failed", validatorResult.Errors.Select(q => q.ErrorMessage).ToList());
            }

            var newProcedure = _mapper.Map<Procedure>(createProcedureRequest);

            newProcedure.IsActive = true;

            var createdProcedure = await _procedureRepository.Create(newProcedure);

            response.Success = true;
            response.Id = createdProcedure.Id;
            response.Message = $"{createdProcedure.Name} procedure created successfully.";

            return response;

        }
    }
}
