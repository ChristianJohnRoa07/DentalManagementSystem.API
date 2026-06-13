using AutoMapper;
using DentalManagementSystem.Application.Contracts.Persistence;
using DentalManagementSystem.Application.DTO.Patients;
using DentalManagementSystem.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Features.Patients.Queries.GetPatientDetailsById
{
    public class GetPatientDetailsByIdHandler : IRequestHandler<GetPatientDetailsByIdQuery, PatientDto>
    {
        private readonly IPatientImageRepository _imageRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public GetPatientDetailsByIdHandler(
            IPatientImageRepository imageRepository,
            IPatientRepository patientRepository,
            IMapper mapper)
        {
            _imageRepository = imageRepository;
            _patientRepository = patientRepository;
            _mapper = mapper;
        }


        public async Task<PatientDto> Handle(GetPatientDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var patientId = request.Id;

            var patient = await _patientRepository.GetPatientWithImages(patientId);

            if (patient == null) 
            { 
                throw NotFoundException.PatientNotFound(patientId);
            }

            var patientDto = _mapper.Map<PatientDto>(patient);

            return patientDto;
        }
    }
}
