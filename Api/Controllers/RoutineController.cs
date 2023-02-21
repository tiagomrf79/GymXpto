using Application.Features.Routines.Commands.CreateRoutine;
using Application.Features.Routines.Commands.DeleteRoutine;
using Application.Features.Routines.Commands.UpdateRoutine;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoutineController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoutineController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Name = "AddRoutine")]
    public async Task<ActionResult<CreateRoutineCommandResponse>> Create([FromBody] CreateRoutineCommand createRoutineCommand)
    {
        var response = await _mediator.Send(createRoutineCommand);
        return Ok(response);
    }

    [HttpPut(Name = "UpdateRoutine")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<UpdateRoutineCommandResponse>> Update([FromBody] UpdateRoutineCommand updateRoutineCommand)
    {
        var response = await _mediator.Send(updateRoutineCommand);
        return Ok(response);
    }

    [HttpDelete("{id}", Name = "DeleteRoutine")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<DeleteRoutineCommandResponse>> Delete(Guid id)
    {
        var deleteRoutineCommand = new DeleteRoutineCommand() { RoutineId = id };
        var response = await _mediator.Send(deleteRoutineCommand);
        return response;
    }
}
