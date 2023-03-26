using Application.Features.Routines.Commands.CreateRoutine;
using Application.Features.Routines.Commands.DeleteRoutine;
using Application.Features.Routines.Commands.UpdateRoutine;
using Application.Features.Routines.Queries.GetRoutineDetail;
using Application.Features.Routines.Queries.GetRoutineList;
using Application.Features.Routines.Queries.GetRoutineListWithWorkouts;
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

    // ActionResult types:
    // Ok => 200 OK
    // CreateAtActionResult => 201 Created (returns also the newly created resource)
    // NotFoundResult => 404 Not Found (record doesn't exist)
    // BadRequestResult => 400 Bad Request (validation failed or unexpected return)
    // UnauthorizedResult => 401 Unauthorized (authentication failed)
    // ForbidResult => 403 Forbidden (authorization failed)
    // NoContentResult => 204 NoContent (no content returned, delete for example)
    // AcceptedResult => Accepted (accepted for processing but not yet complete)
    // ConflictResult => Conflict (create resource that already exists, for example)
    // ActionResult<...>

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
    public async Task<ActionResult<List<RoutineListDto>>> GetAllRoutines()
    {
        var getRoutinesListQuery = new GetRoutineListQuery();
        var response = await _mediator.Send(getRoutinesListQuery);
        return Ok(response);
    }

    [HttpGet("allwithworkouts", Name = "GetAllRoutinesWithWorkouts")]
    public async Task<ActionResult<List<RoutineWorkoutsListDto>>> GetAllRoutinesWithWorkouts()
    {
        var getRoutinesListWithWorkoutsQuery = new GetRoutineListWithWorkoutsQuery();
        var response = await _mediator.Send(getRoutinesListWithWorkoutsQuery);
        return Ok(response);
    }
}
