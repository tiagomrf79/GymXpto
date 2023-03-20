namespace Application.Features.Equipments.Commands.UpdateEquipment;

public class UpdateEquipmentDto
{
    public Guid EquipmentId { get; set; }
    public string Name { get; set; } = string.Empty;
}