using Application.Features.ExerciseSets.Commands.CreateExerciseSet;
using Application.Features.ExerciseSets.Commands.DeleteExerciseSet;
using Application.Features.ExerciseSets.Commands.UpdateExerciseSet;
using Application.Features.ExerciseSets.Queries.GetExerciseSetDetail;
using Application.Features.ExerciseSets.Queries.GetExerciseSetsFromSuperset;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExerciseSetController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExerciseSetController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Name = "AddExerciseSet")]
    public async Task<ActionResult<CreateExerciseSetCommandResponse>> Create([FromBody] CreateExerciseSetCommand createExerciseSetCommand)
    {
        var response = await _mediator.Send(createExerciseSetCommand);
        return Ok(response);
    }

    [HttpPut(Name = "UpdateExerciseSet")]
    public async Task<ActionResult<UpdateExerciseSetCommandResponse>> Update([FromBody] UpdateExerciseSetCommand updateExerciseSetCommand)
    {
        var response = await _mediator.Send(updateExerciseSetCommand);
        return Ok(response);
    }

    [HttpDelete("{id}", Name = "DeleteExerciseSet")]
    public async Task<ActionResult<DeleteExerciseSetCommandResponse>> Delete(Guid id)
    {
        var command = new DeleteExerciseSetCommand { ExerciseSetId = id };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpGet("{id}", Name = "GetExerciseSetById")]
    public async Task<ActionResult<GetExerciseSetDetailQueryResponse>> GetExerciseSetById(Guid id)
    {
        var query = new GetExerciseSetDetailQuery { ExerciseSetId = id };
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("all/{id}", Name = "GetSupersetExerciseSets")]
    public async Task<ActionResult<GetExerciseSetsFromSupersetQueryResponse>> GetSupersetExerciseSets(Guid id)
    {
        var query = new GetExerciseSetsFromSupersetQuery { SupersetId = id };
        var response = await _mediator.Send(query);
        return Ok(response);
    }
}
