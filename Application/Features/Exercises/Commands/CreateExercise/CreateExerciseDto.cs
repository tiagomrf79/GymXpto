using Domain.Enums;

namespace Application.Features.Exercises.Commands.CreateExercise;

public class CreateExerciseDto
{
    public Guid ExerciseId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Instructions { get; set; }
    public int? UtilityType { get; set; }
    public int MechanicType { get; set; }
    public int MovementType { get; set; }
    public Guid? MainEquipmentUsedId { get; set; }
    public string? Comments { get; set; }
}