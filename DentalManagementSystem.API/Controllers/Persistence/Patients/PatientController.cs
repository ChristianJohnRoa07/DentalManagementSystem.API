using DentalManagementSystem.Application.DTO.Patients;
using DentalManagementSystem.Application.DTO.Procedures;
using DentalManagementSystem.Application.DTO.Responses;
using DentalManagementSystem.Application.Features.Patients.Command.Create;
using DentalManagementSystem.Application.Features.Patients.Command.Update;
using DentalManagementSystem.Application.Features.Patients.Command.Upload;
using DentalManagementSystem.Application.Features.Patients.Queries.GetPatientDetailsById;
using DentalManagementSystem.Application.Features.Patients.Queries.GetUploadedImagePerPatients;
using DentalManagementSystem.Application.Features.Procedures.Command.Create;
using DentalManagementSystem.Application.Features.Procedures.Command.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalManagementSystem.API.Controllers.Persistence.Patients
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid Id)
        {
            var query = new GetPatientDetailsByIdQuery { Id = Id };
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("images/{Id:guid}")]
        public async Task<IActionResult> GetPatientImages([FromRoute] Guid Id)
        {
            var query = new GetUploadedImagePerPatientQuery { PatientId = Id };
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePatientDto createPatientDto)
        {
            var command = new CreatePatientCommand { createPatientDto = createPatientDto };

            var response = await _mediator.Send(command);

            var result = new BaseApiResponse<BaseCommandResponse>(response, StatusCodes.Status201Created);

            return Ok(result);
        }

        [HttpPost("upload-images/{Id:guid}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImages([FromRoute] Guid Id, [FromForm] IFormFileCollection files)
        {
            var command = new UploadPatientImageCommand
            {
                PatientId = Id,
                Files = files,
            };

            var response = await _mediator.Send(command);

            var result = new BaseApiResponse<BaseCommandResponse>(response, StatusCodes.Status200OK);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePatientDto updatePatientDto)
        {
            var command = new UpdatePatientCommand { updatePatientDto = updatePatientDto };

            var response = await _mediator.Send(command);

            var result = new BaseApiResponse<BaseCommandResponse>(response, StatusCodes.Status200OK);

            return Ok(result);
        }
    }
}
