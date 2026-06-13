using DentalManagementSystem.Application.Contracts.Identity;
using DentalManagementSystem.Application.Contracts.Persistence;
using DentalManagementSystem.Application.DTO.Procedures;
using DentalManagementSystem.Application.DTO.Responses;
using DentalManagementSystem.Application.Features.Procedures.Command.ChangeActiveStatus;
using DentalManagementSystem.Application.Features.Procedures.Command.Create;
using DentalManagementSystem.Application.Features.Procedures.Command.Update;
using DentalManagementSystem.Application.Features.Procedures.Queries.Get;
using DentalManagementSystem.Application.Features.Procedures.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalManagementSystem.API.Controllers.Persistence.Procedures
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProcedureController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProcedureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<BaseApiResponse<List<ProcedureDto>>>> Get()
        {
            var response = await _mediator.Send(new GetProcedureQuery());

            var result = new BaseApiResponse<List<ProcedureDto>>(response, StatusCodes.Status200OK);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BaseApiResponse<ProcedureDto>>> GetById([FromRoute] Guid id)
        {
            var command = new GetProcedureByIdQuery { Id = id };

            var response = await _mediator.Send(command);

            var result = new BaseApiResponse<ProcedureDto>(response, StatusCodes.Status200OK);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProcedureDto createProcedureDto)
        {
            var command = new CreateProcedureCommand { createProcedureDto = createProcedureDto };

            var response = await _mediator.Send(command);

            var result = new BaseApiResponse<BaseCommandResponse>(response, StatusCodes.Status201Created);

            return Ok(result);
        }

        [HttpPost("change-status")]
        public async Task<IActionResult> ChangeProcedureStatus([FromBody] ChangeActiveStatusDto changeApprovalStatusDto)
        {
            var command = new ChangeActiveStatusCommand { changeActiveStatusDto = changeApprovalStatusDto };

            var response = await _mediator.Send(command);

            var result = new BaseApiResponse<BaseCommandResponse>(response, StatusCodes.Status200OK);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProcedureDto updateProcedureDto)
        {
            var command = new UpdateProcedureCommand { updateProcedureDto = updateProcedureDto };

            var response = await _mediator.Send(command);

            var result = new BaseApiResponse<BaseCommandResponse>(response, StatusCodes.Status200OK);

            return Ok(result);
        }
    }
}
