using Application.Features.Groups.Commands.CreateGroup;
using Application.Features.Groups.Commands.DeleteGroup;
using Application.Features.Groups.Commands.UpdateGroup;
using Application.Features.Groups.Queries.GetGroupDetail;
using Application.Features.Groups.Queries.GetGroupsFromWorkout;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupController : ControllerBase
{
    private readonly IMediator _mediator;

    public GroupController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Name = "AddGroup")]
    public async Task<ActionResult<CreateGroupCommandResponse>> Create([FromBody] CreateGroupCommand createGroupCommand)
    {
        var response = await _mediator.Send(createGroupCommand);
        return Ok(response);
    }

    [HttpPut(Name = "UpdateGroup")]
    public async Task<ActionResult<UpdateGroupCommandResponse>> Update([FromBody] UpdateGroupCommand updateGroupCommand)
    {
        var response = await _mediator.Send(updateGroupCommand);
        return Ok(response);
    }

    [HttpDelete("{id}", Name = "DeleteGroup")]
    public async Task<ActionResult<DeleteGroupCommandResponse>> Delete(Guid id)
    {
        var command = new DeleteGroupCommand { GroupId = id };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpGet("{id}", Name = "GetGroupById")]
    public async Task<ActionResult<GetGroupDetailQueryResponse>> GetGroupById(Guid id)
    {
        var query = new GetGroupDetailQuery { GroupId = id };
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("all/{id}", Name = "GetWorkoutGroups")]
    public async Task<ActionResult<GetGroupsFromWorkoutQueryResponse>> GetWorkoutGroups(Guid id)
    {
        var query = new GetGroupsFromWorkoutQuery { WorkoutId = id };
        var response = await _mediator.Send(query);
        return Ok(response);
    }
}
