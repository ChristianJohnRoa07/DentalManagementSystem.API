using DentalManagementSystem.Application.Contracts.Identity;
using DentalManagementSystem.Application.Contracts.Persistence;
using DentalManagementSystem.Application.DTO.Procedures;
using DentalManagementSystem.Application.DTO.Responses;
using DentalManagementSystem.Application.Features.Procedures.Command.ChangeActiveStatus;
using DentalManagementSystem.Application.Features.Procedures.Command.Create;
using DentalManagementSystem.Application.Features.Procedures.Command.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DentalManagementSystem.API.Controllers.Persistence.Procedures
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcedureController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProcedureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateProcedure")]
        public async Task<IActionResult> Create([FromBody] CreateProcedureDto createProcedureDto)
        {
            var command = new CreateProcedureCommand { createProcedureDto = createProcedureDto };

            var response = await _mediator.Send(command);

            var result = new BaseApiResponse<BaseCommandResponse>(response, StatusCodes.Status201Created);

            return Ok(result);
        }

        [HttpPost("UpdateProcedure")]
        public async Task<IActionResult> Update([FromBody] UpdateProcedureDto updateProcedureDto)
        {
            var command = new UpdateProcedureCommand { updateProcedureDto = updateProcedureDto };

            var response = await _mediator.Send(command);

            var result = new BaseApiResponse<BaseCommandResponse>(response, StatusCodes.Status201Created);

            return Ok(result);
        }

        [HttpPost("ChangeProcedureStatus")]
        public async Task<IActionResult> ChangeProcedureStatus([FromBody] ChangeActiveStatusDto changeApprovalStatusDto)
        {
            var command = new ChangeActiveStatusCommand { changeActiveStatusDto = changeApprovalStatusDto };

            var response = await _mediator.Send(command);

            var result = new BaseApiResponse<BaseCommandResponse>(response, StatusCodes.Status201Created);

            return Ok(result);
        }

    }
}
