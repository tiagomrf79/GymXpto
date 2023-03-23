using Application.Features.Workouts.Commands.CreateWorkout;
using Application.Features.Workouts.Commands.DeleteWorkout;
using Application.Features.Workouts.Commands.UpdateWorkout;
using Application.Features.Workouts.Queries.GetWorkoutDetail;
using Application.Features.Workouts.Queries.GetWorkoutsFromRoutine;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkoutController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkoutController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Name = "AddWorkout")]
    public async Task<ActionResult<CreateWorkoutCommandResponse>> Create([FromBody] CreateWorkoutCommand createWorkoutCommand)
    {
        var response = await _mediator.Send(createWorkoutCommand);
        return Ok(response);
    }

    [HttpPut(Name = "UpdateWorkout")]
    public async Task<ActionResult<UpdateWorkoutCommandResponse>> Update([FromBody] UpdateWorkoutCommand updateWorkoutCommand)
    {
        var response = await _mediator.Send(updateWorkoutCommand);
        return Ok(response);
    }

    [HttpDelete("{id}", Name = "DeleteWorkout")]
    public async Task<ActionResult<DeleteWorkoutCommandResponse>> Delete(Guid id)
    {
        var command = new DeleteWorkoutCommand { WorkoutId = id };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpGet("{id}", Name = "GetWorkoutById")]
    public async Task<ActionResult<GetWorkoutDetailQueryResponse>> GetWorkoutById(Guid id)
    {
        var query = new GetWorkoutDetailQuery() { WorkoutId = id };
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("all/{id}", Name = "GetRoutineWorkouts")]
    public async Task<ActionResult<GetWorkoutsFromRoutineQueryResponse>> GetRoutineWorkouts(Guid id)
    {
        var query = new GetWorkoutsFromRoutineQuery() { RoutineId = id };
        var response = await _mediator.Send(query);
        return Ok(response);
    }
}
