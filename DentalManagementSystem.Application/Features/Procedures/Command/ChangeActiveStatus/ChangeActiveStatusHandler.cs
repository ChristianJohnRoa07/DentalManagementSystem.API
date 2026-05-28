using AutoMapper;
using DentalManagementSystem.Application.Contracts.Persistence;
using DentalManagementSystem.Application.DTO.Procedure.Validator;
using DentalManagementSystem.Application.DTO.Responses;
using DentalManagementSystem.Application.Exceptions;
using FluentValidation;
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
                throw new BadRequestException("Validation failed", validatorResult.Errors.Select(q => q.ErrorMessage).ToList());
            }

            var procedureId = request.changeActiveStatusDto.procedureId;
            await _procedureRepository.ChangeProcedureStatus(procedureId);

            response.Success = true;
            response.Id = procedureId;
            response.Message = "Procedure status updated successfully.";

            return response;
        }
    }
}
