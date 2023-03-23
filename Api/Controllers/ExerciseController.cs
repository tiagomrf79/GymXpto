using Application.Features.Exercises.Commands.CreateExercise;
using Application.Features.Exercises.Commands.DeleteExercise;
using Application.Features.Exercises.Commands.UpdateExercise;
using Application.Features.Exercises.Queries.GetExerciseDetail;
using Application.Features.Exercises.Queries.GetExerciseList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExerciseController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExerciseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Name = "AddExercise")]
    public async Task<ActionResult<CreateExerciseCommandResponse>> Create([FromBody] CreateExerciseCommand createExerciseCommand)
    {
        var response = await _mediator.Send(createExerciseCommand);
        return Ok(response);
    }

    [HttpPut(Name = "UpdateExercise")]
    public async Task<ActionResult<UpdateExerciseCommandResponse>> Update([FromBody] UpdateExerciseCommand updateExerciseCommand)
    {
        var response = await _mediator.Send(updateExerciseCommand);
        return Ok(response);
    }

    [HttpDelete("{id}", Name = "DeleteExercise")]
    public async Task<ActionResult<DeleteExerciseCommandResponse>> Delete(Guid id)
    {
        var command = new DeleteExerciseCommand { ExerciseId = id };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpGet("{id}", Name = "GetExerciseById")]
    public async Task<ActionResult<GetExerciseDetailQueryResponse>> GetExerciseById(Guid id)
    {
        var query = new GetExerciseDetailQuery { ExerciseId = id };
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("all", Name = "GetAllExercises")]
    public async Task<ActionResult<GetExerciseListQueryResponse>> GetAllExercises()
    {
        var query = new GetExerciseListQuery();
        var response = await _mediator.Send(query);
        return Ok(response);
    }
}
