using Application.Features.Equipments.Commands.CreateEquipment;
using Application.Features.Equipments.Commands.DeleteEquipment;
using Application.Features.Equipments.Commands.UpdateEquipment;
using Application.Features.Equipments.Queries.GetEquipmentList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EquipmentController : ControllerBase //ControllerBase is more lightweight than Controller
{
    private readonly IMediator _mediator;

    public EquipmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Name = "AddEquipment")]
    public async Task<ActionResult<CreateEquipmentCommandResponse>> Add([FromBody] CreateEquipmentCommand createEquipmentCommand)
    {
        var response = await _mediator.Send(createEquipmentCommand);
        return Ok(response);
    }

    [HttpPut(Name = "UpdateEquipment")]
    public async Task<ActionResult<UpdateEquipmentCommandResponse>> Update([FromBody] UpdateEquipmentCommand updateEquipmentCommand)
    {
        var response = await _mediator.Send(updateEquipmentCommand);
        return Ok(response);
    }

    [HttpDelete("{id}", Name = "DeleteEquipment")]
    public async Task<ActionResult<DeleteEquipmentCommandResponse>> Delete(Guid id)
    {
        var command = new DeleteEquipmentCommand { EquipmentId = id };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpGet("all", Name = "GetAllEquipments")]
    public async Task<ActionResult<GetEquipmentListQueryResponse>> GetAllEquipments()
    {
        var query = new GetEquipmentListQuery();
        var response = await _mediator.Send(query);
        return Ok(response);
    }
}
