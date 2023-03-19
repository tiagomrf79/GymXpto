using Domain.Common;

namespace Domain.Entities;

public class Muscle : AuditableEntity
{
    public Guid MuscleId { get; set; }
    public string Name { get; set; } = string.Empty;
    
    //TODO: property with muscle image or property with position on body image map
}
