namespace Application.Features.Exercises.Queries.GetExerciseList;

public class ExerciseListVm
{
    public Guid ExerciseId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Instructions { get; set; }
    public int? UtilityType { get; set; }
    public int MechanicType { get; set; }
    public int MovementType { get; set; }
    public Guid MainMuscleWorkedId { get; set; }
    public Guid? MainEquipmentUsedId { get; set; }
    public string? Comments { get; set; }
}