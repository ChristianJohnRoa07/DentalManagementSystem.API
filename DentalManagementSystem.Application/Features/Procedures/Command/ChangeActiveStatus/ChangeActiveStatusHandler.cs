using AutoMapper;
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

namespace DentalManagementSystem.Application.Features.Procedures.Command.ChangeActiveStatus
{
    public class ChangeActiveStatusHandler : IRequestHandler<ChangeActiveStatusCommand, BaseCommandResponse>
    {
        private readonly IProcedureRepository _procedureRepository;
        private readonly IMapper _mapper;

        public ChangeActiveStatusHandler(IProcedureRepository procedureRepository, IMapper mapper)
        {
            _procedureRepository = procedureRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(ChangeActiveStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new ChangeActiveStatusDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.changeActiveStatusDto, cancellationToken);

            if (!validatorResult.IsValid)
            {
                throw new ValidationException(validatorResult);
            }

            var procedureId = request.changeActiveStatusDto.procedureId;
            var procedure = await _procedureRepository.GetById(procedureId);

            if (procedure == null) 
            {
                throw new NotFoundException($"Procedure with ID {procedureId} was not found.");
            }

            await _procedureRepository.ChangeProcedureStatus(procedure.Id);

            response.Id = procedure.Id;
            response.Message = "Procedure status updated successfully.";

            return response;
        }
    }
}
