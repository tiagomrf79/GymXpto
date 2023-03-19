using Domain.Common;

namespace Domain.Entities;

public class Equipment : AuditableEntity
{
    public Guid EquipmentId { get; set; }
    public string Name { get; set; } = string.Empty;

}
