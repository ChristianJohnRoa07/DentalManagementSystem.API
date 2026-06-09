using DentalManagementSystem.Application.DTO.Patients;
using DentalManagementSystem.Application.DTO.Procedures;
using DentalManagementSystem.Application.DTO.Responses;
using DentalManagementSystem.Application.Features.Patients.Command.Create;
using DentalManagementSystem.Application.Features.Patients.Command.Update;
using DentalManagementSystem.Application.Features.Procedures.Command.Create;
using DentalManagementSystem.Application.Features.Procedures.Command.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DentalManagementSystem.API.Controllers.Persistence.Patients
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePatientDto createPatientDto)
        {
            var command = new CreatePatientCommand { createPatientDto = createPatientDto };

            var response = await _mediator.Send(command);

            var result = new BaseApiResponse<BaseCommandResponse>(response, StatusCodes.Status201Created);

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
