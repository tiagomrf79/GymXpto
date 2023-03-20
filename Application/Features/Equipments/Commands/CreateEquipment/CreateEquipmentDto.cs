namespace Application.Features.Equipments.Commands.CreateEquipment;

public class CreateEquipmentDto
{
    public Guid EquipmentId { get; set; }
    public string Name { get; set; } = string.Empty;
}