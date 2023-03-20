using Application.Responses;

namespace Application.Features.Equipments.Commands.UpdateEquipment;

public class UpdateEquipmentCommandResponse : BaseResponse
{
    public UpdateEquipmentDto Equipment { get; set; } = default!;
}