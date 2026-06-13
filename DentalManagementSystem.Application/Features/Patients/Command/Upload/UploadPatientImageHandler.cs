using DentalManagementSystem.Application.Contracts.Infrastructure;
using DentalManagementSystem.Application.Contracts.Persistence;
using DentalManagementSystem.Application.DTO.Responses;
using DentalManagementSystem.Application.Exceptions;
using DentalManagementSystem.Domain.Entities;
using MediatR;

namespace DentalManagementSystem.Application.Features.Patients.Command.Upload
{
    public class UploadPatientImageHandler : IRequestHandler<UploadPatientImageCommand, BaseCommandResponse>
    {
        private readonly IPatientImageRepository _imageRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IFileStorageService _storageService;

        public UploadPatientImageHandler(
            IPatientImageRepository imageRepository,
            IPatientRepository patientRepository,
            IFileStorageService storageService)
        {
            _imageRepository = imageRepository;
            _patientRepository = patientRepository;
            _storageService = storageService;
        }

        public async Task<BaseCommandResponse> Handle(UploadPatientImageCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            // Verify the patient actually exists first
            var patientExists = await _patientRepository.GetById(request.PatientId);
            if (patientExists == null)
            {
                throw NotFoundException.PatientNotFound(request.PatientId); 
            }

            if (request.Files == null || request.Files.Count == 0)
            {
                throw new BadRequestException("No files were selected for upload.");
            }

            var savedImages = new List<PatientImage>();

            // Loop through each uploaded image file sequentially 
            foreach (var file in request.Files)
            {
                // Save file onto local disk storage via service container
                var savedFilePath = await _storageService.UploadFileAsync(file, "patients");

                var patientImage = new PatientImage
                {
                    PatientId = request.PatientId,
                    FilePath = savedFilePath,
                    FileName = file.FileName,
                };

                // Add record to db context tracking graph
                await _imageRepository.Create(patientImage);
                savedImages.Add(patientImage);
            }

            response.Message = $"{savedImages.Count} images uploaded and attached to patient successfully.";
            return response;
        }
    }
}
