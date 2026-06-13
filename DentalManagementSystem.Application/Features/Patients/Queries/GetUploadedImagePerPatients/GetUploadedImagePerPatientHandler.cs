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

namespace DentalManagementSystem.Application.Features.Patients.Queries.GetUploadedImagePerPatients
{
    public class GetUploadedImagesPerPatientHandler : IRequestHandler<GetUploadedImagePerPatientQuery, List<PatientImageDto>>
    {
        private readonly IPatientImageRepository _imageRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public GetUploadedImagesPerPatientHandler(
            IPatientImageRepository imageRepository,
            IPatientRepository patientRepository,
            IMapper mapper)
        {
            _imageRepository = imageRepository;
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<List<PatientImageDto>> Handle(GetUploadedImagePerPatientQuery request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetById(request.PatientId);
            if (patient == null)
            {
                throw NotFoundException.PatientNotFound(request.PatientId);
            }

            var images = await _imageRepository.GetImagesByPatientId(request.PatientId);
            
            var imageDtos = _mapper.Map<List<PatientImageDto>>(images);

            return imageDtos;
        }
    }
}
