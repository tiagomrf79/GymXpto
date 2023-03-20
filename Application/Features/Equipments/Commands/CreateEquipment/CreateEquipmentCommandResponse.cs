using Application.Responses;

namespace Application.Features.Equipments.Commands.CreateEquipment;

public class CreateEquipmentCommandResponse : BaseResponse
{
    public CreateEquipmentDto Equipment { get; set; } = default!;
}