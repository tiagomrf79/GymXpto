using Application.Features.Supersets.Commands.CreateSuperset;
using Application.Features.Supersets.Commands.DeleteSuperset;
using Application.Features.Supersets.Commands.UpdateSuperset;
using Application.Features.Supersets.Queries.GetSupersetDetail;
using Application.Features.Supersets.Queries.GetSupersetsFromGroup;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupersetController : ControllerBase
{
    private readonly IMediator _mediator;

    public SupersetController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Name = "AddSuperset")]
    public async Task<ActionResult<CreateSupersetCommandResponse>> Create([FromBody] CreateSupersetCommand createSupersetCommand)
    {
        var response = await _mediator.Send(createSupersetCommand);
        return Ok(response);
    }

    [HttpPut(Name = "UpdateSuperset")]
    public async Task<ActionResult<UpdateSupersetCommandResponse>> Update([FromBody] UpdateSupersetCommand updateSupersetCommand)
    {
        var response = await _mediator.Send(updateSupersetCommand);
        return Ok(response);
    }

    [HttpDelete("{id}", Name = "DeleteSuperset")]
    public async Task<ActionResult<DeleteSupersetCommandResponse>> Delete(Guid id)
    {
        var command = new DeleteSupersetCommand { SupersetId = id };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpGet("{id}", Name = "GetSupersetById")]
    public async Task<ActionResult<GetSupersetDetailQueryResponse>> GetSupersetById(Guid id)
    {
        var query = new GetSupersetDetailQuery { SupersetId = id };
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("all/{id}", Name = "GetGroupSupersets")]
    public async Task<ActionResult<GetSupersetsFromGroupQueryResponse>> GetGroupSupersets(Guid id)
    {
        var query = new GetSupersetsFromGroupQuery { GroupId = id };
        var response = await _mediator.Send(query);
        return Ok(response);
    }
}
