using Application.Features.Muscles.Commands.CreateMuscle;
using Application.Features.Muscles.Commands.DeleteMuscle;
using Application.Features.Muscles.Commands.UpdateMuscle;
using Application.Features.Muscles.Queries.GetMuscleList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MuscleController : ControllerBase
{
    private readonly IMediator _mediator;

    public MuscleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Name = "AddMuscle")]
    public async Task<ActionResult<CreateMuscleCommandResponse>> Add([FromBody] CreateMuscleCommand createMuscleCommand)
    {
        var response = await _mediator.Send(createMuscleCommand);
        return Ok(response);
    }

    [HttpPut(Name = "UpdateMuscle")]
    public async Task<ActionResult<UpdateMuscleCommandResponse>> Update([FromBody] UpdateMuscleCommand updateMuscleCommand)
    {
        var response = await _mediator.Send(updateMuscleCommand);
        return Ok(response);
    }

    [HttpDelete("{id}", Name = "DeleteMuscle")]
    public async Task<ActionResult<DeleteMuscleCommandResponse>> Delete(Guid id)
    {
        var command = new DeleteMuscleCommand { MuscleId = id };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpGet("all", Name = "GetAllMuscles")]
    public async Task<ActionResult<GetMuscleListQueryResponse>> GetAllMuscles()
    {
        var query = new GetMuscleListQuery();
        var response = await _mediator.Send(query);
        return Ok(response);
    }
}
