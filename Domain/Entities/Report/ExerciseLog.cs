namespace Domain.Entities.Report;

/// <summary>
/// One 'ExerciseLog' represents the results of executing one set of one exercise
/// </summary>
public class ExerciseLog
{
    public Guid Id { get; set; }
    public Guid WorkoutLogId { get; set; }
    public int Order { get; set; } //position in workout
    public string ExerciseName { get; set; } = string.Empty;
    public Muscle MainMuscleWorked { get; set; }
    public int Repetitions { get; set; }
    public int WeightInKg { get; set; } //weight per rep
}