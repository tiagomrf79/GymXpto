namespace Domain.Entities;

public class ExerciseMuscles
{
    public Guid ExerciseId { get; set; }
    public Exercise Exercise { get; set; } = null!;
    public Guid MuscleId { get; set; }
    public Muscle Muscle { get; set;} = null!;
    public bool IsMainMuscle { get; set; }
}
