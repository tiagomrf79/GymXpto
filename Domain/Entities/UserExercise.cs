namespace Domain.Entities;

public class UserExercise : Exercise
{
    public Guid UserExerciseId { get; set; }
    public Guid UserId { get; set; }
}
