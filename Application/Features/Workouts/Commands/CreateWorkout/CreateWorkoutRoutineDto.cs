namespace Application.Features.Workouts.Commands.CreateWorkout;

public class CreateWorkoutRoutineDto
{
    public Guid RoutineId { get; set; }
    public string Title { get; set; } = string.Empty;
}