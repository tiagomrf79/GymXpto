using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

/// <summary>
/// One 'Exercise' represents one type of exercise
/// For example: Barbell Squat
/// </summary>
public class Exercise : AuditableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Instructions { get; set; }
    public UtilityTypes? UtilityType { get; set; }
    public MechanicTypes MechanicType { get; set; }
    public MovementTypes MovementType { get; set; }
    public Muscle MainMuscleWorked { get; set; } = default!;
    public IList<Muscle>? SynergistsMusclesWorked { get; private set; } = new List<Muscle>();
    public Equipment? MainEquipmentUsed { get; set; }
    public string? Comments { get; set; }
    
    //TODO: for exercise execution: property with image or GIF or video or sequence of images?
}
