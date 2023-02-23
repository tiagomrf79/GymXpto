namespace Application.Features.Routines.Queries.GetRoutinesListWithWorkouts;

public class RoutineWorkoutDto
{
    public Guid WorkoutId { get; set; }
    public Guid RoutineId { get; set; }
    public string Title { get; set; } = string.Empty;
}