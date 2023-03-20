using MediatR;

namespace Application.Features.Equipments.Commands.UpdateEquipment;

public class UpdateEquipmentCommand : IRequest<UpdateEquipmentCommandResponse>
{
    public Guid EquipmentId { get; set; }
    public string Name { get; set; } = string.Empty;
}
