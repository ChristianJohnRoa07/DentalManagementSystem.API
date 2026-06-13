using DentalManagementSystem.Application.DTO.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DentalManagementSystem.Application.Features.Patients.Command.Upload
{
    public class UploadPatientImageCommand : IRequest<BaseCommandResponse>
    {
        public Guid PatientId { get; set; }
        public IFormFileCollection Files { get; set; } = null!;
    }
}
