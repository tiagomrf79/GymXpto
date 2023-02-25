using Application.Features.Routines.Commands.CreateRoutine;
using Application.Features.Routines.Commands.DeleteRoutine;
using Application.Features.Routines.Commands.UpdateRoutine;
using Application.Features.Routines.Queries.GetRoutineDetail;
using Application.Features.Routines.Queries.GetRoutinesList;
using Application.Features.Routines.Queries.GetRoutinesListWithWorkouts;
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
    public async Task<ActionResult<UpdateRoutineCommandResponse>> Update([FromBody] UpdateRoutineCommand updateRoutineCommand)
    {
        var response = await _mediator.Send(updateRoutineCommand);
        return Ok(response);
    }

    [HttpDelete("{id}", Name = "DeleteRoutine")]
    public async Task<ActionResult<DeleteRoutineCommandResponse>> Delete(Guid id)
    {
        var deleteRoutineCommand = new DeleteRoutineCommand() { RoutineId = id };
        var response = await _mediator.Send(deleteRoutineCommand);
        return Ok(response);
    }

    [HttpGet("{id}", Name = "GetRoutineById")]
    public async Task<ActionResult<GetRoutineDetailQueryResponse>> GetRoutineById(Guid id)
    {
        var getRoutineDetailQuery = new GetRoutineDetailQuery() { RoutineId = id };
        var response = await _mediator.Send(getRoutineDetailQuery);
        return Ok(response);
    }

    [HttpGet("all", Name = "GetAllRoutines")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<RoutineListVm>>> GetAllRoutines()
    {
        var getRoutinesListQuery = new GetRoutinesListQuery();
        var response = await _mediator.Send(getRoutinesListQuery);
        return Ok(response);
    }

    [HttpGet("allwithworkouts", Name = "GetAllRoutinesWithWorkouts")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<RoutineWorkoutsListVm>>> GetAllRoutinesWithWorkouts()
    {
        var getRoutinesListWithWorkoutsQuery = new GetRoutinesListWithWorkoutsQuery();
        var response = await _mediator.Send(getRoutinesListWithWorkoutsQuery);
        return Ok(response);
    }
}
