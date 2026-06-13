using AutoMapper;
using DentalManagementSystem.Application.Contracts.Persistence;
using DentalManagementSystem.Application.DTO.Patients.Validator;
using DentalManagementSystem.Application.DTO.Responses;
using DentalManagementSystem.Application.Exceptions;
using DentalManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Features.Patients.Command.Create
{
    public class CreatePatientHandler : IRequestHandler<CreatePatientCommand, BaseCommandResponse>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public CreatePatientHandler(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var createPatient = request.createPatientDto;
            var response = new BaseCommandResponse();
            var validator = new CreatePatientDtoValidator();
            var validatorResult = await validator.ValidateAsync(createPatient, cancellationToken);

            if (!validatorResult.IsValid)
            {
                throw new ValidationException(validatorResult);
            }

            var newPatient = _mapper.Map<Patient>(createPatient);

            var createdPatient = await _patientRepository.Create(newPatient);

            response.Message = $"Patient {createdPatient.LastName}, {createdPatient.FirstName} created successfully.";

            return response;
        }
    }
}
