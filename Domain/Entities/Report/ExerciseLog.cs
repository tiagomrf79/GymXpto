using Domain.Common;

namespace Domain.Entities.Report;

/// <summary>
/// One 'ExerciseLog' represents the results of executing one set of one exercise
/// </summary>
public class ExerciseLog : AuditableEntity
{
    public Guid Id { get; set; }
    public Guid WorkoutLogId { get; set; }
    public int Order { get; set; } //position in workout
    public string ExerciseName { get; set; } = string.Empty;
    public Muscle MainMuscleWorked { get; set; } = default!;
    public int Repetitions { get; set; }
    public int WeightInKg { get; set; } //weight per rep
}