using AutoMapper;
using DentalManagementSystem.Application.Contracts.Persistence;
using DentalManagementSystem.Application.DTO.Patients.Validator;
using DentalManagementSystem.Application.DTO.Responses;
using DentalManagementSystem.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Features.Patients.Command.Update
{
    public class UpdatePatientHandler : IRequestHandler<UpdatePatientCommand, BaseCommandResponse>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public UpdatePatientHandler(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var updatePatientRequest = request.updatePatientDto;
            var response = new BaseCommandResponse();

            var validator = new UpdatePatientDtoValidator();
            var validatorResult = await validator.ValidateAsync(updatePatientRequest, cancellationToken);
            if (!validatorResult.IsValid)
            {
                throw new ValidationException(validatorResult);
            }

            var existingPatient = await _patientRepository.GetById(updatePatientRequest.Id);
            if(existingPatient == null)
            {
                throw NotFoundException.PatientNotFound(updatePatientRequest.Id);
            }

            _mapper.Map(updatePatientRequest, existingPatient);

            await _patientRepository.Update(existingPatient);

            response.Id = existingPatient.Id;
            response.Message = $"{existingPatient.LastName}, {existingPatient.FirstName} patient updated successfully.";

            return response;
        }
    }
}
