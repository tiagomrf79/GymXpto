using MediatR;

namespace Application.Features.Equipments.Commands.DeleteEquipment;

public class DeleteEquipmentCommand : IRequest<DeleteEquipmentCommandResponse>
{
    public Guid EquipmentId { get; set; }
}
