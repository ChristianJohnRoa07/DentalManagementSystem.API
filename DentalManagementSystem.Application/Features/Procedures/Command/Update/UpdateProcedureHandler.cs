using AutoMapper;
using DentalManagementSystem.Application.Contracts.Persistence;
using DentalManagementSystem.Application.DTO.Procedures.Validator;
using DentalManagementSystem.Application.DTO.Responses;
using DentalManagementSystem.Application.Exceptions;
using DentalManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Features.Procedures.Command.Update
{
    public class UpdateProcedureHandler : IRequestHandler<UpdateProcedureCommand, BaseCommandResponse>
    {
        private readonly IProcedureRepository _procedureRepository;
        private readonly IMapper _mapper;

        public UpdateProcedureHandler(IProcedureRepository procedureRepository, IMapper mapper)
        {
            _procedureRepository = procedureRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateProcedureCommand request, CancellationToken cancellationToken)
        {
            var updateProcedureRequest = request.updateProcedureDto;
            var response = new BaseCommandResponse();
            var validator = new UpdateProcedureValidator();
            var validatorResult = await validator.ValidateAsync(updateProcedureRequest, cancellationToken);

            if (!validatorResult.IsValid)
            {
                throw new ValidationException(validatorResult);
            }

            var existingProcedure = await _procedureRepository.GetById(updateProcedureRequest.Id);
            if (existingProcedure == null)
            {
                throw NotFoundException.ProcedureNotFound(updateProcedureRequest.Id);
            }

            _mapper.Map(updateProcedureRequest, existingProcedure);

            await _procedureRepository.Update(existingProcedure);

            response.Id = existingProcedure.Id;
            response.Message = $"{existingProcedure.Name} procedure updated successfully.";

            return response;
        }
    }
}
