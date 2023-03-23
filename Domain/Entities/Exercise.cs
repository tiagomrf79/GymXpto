using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

/// <summary>
/// One 'Exercise' represents one type of exercise
/// For example: Barbell Squat
/// </summary>
public class Exercise : AuditableEntity
{
    public Guid ExerciseId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Instructions { get; set; }
    public UtilityTypes? UtilityType { get; set; }
    public MechanicTypes MechanicType { get; set; }
    public MovementTypes MovementType { get; set; }
    public Guid MainMuscleWorkedId { get; set; }
    public Muscle MainMuscleWorked { get; set; } = null!;
    public List<Muscle>? SynergistsMusclesWorked { get; private set; } = new List<Muscle>();
    public Guid? MainEquipmentUsedId { get; set; }
    public Equipment? MainEquipmentUsed { get; set; }
    public string? Comments { get; set; }
    
    //TODO: for exercise execution: property with image or GIF or video or sequence of images?
}
