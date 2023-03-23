namespace Application.Features.Workouts.Commands.UpdateWorkout;

public class UpdateWorkoutRoutineDto
{
    public Guid RoutineId { get; set; }
    public string Title { get; set; } = string.Empty;
}