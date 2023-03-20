using MediatR;

namespace Application.Features.Equipments.Commands.CreateEquipment;

public class CreateEquipmentCommand : IRequest<CreateEquipmentCommandResponse>
{
    public string Name { get; set; } = string.Empty;
}
