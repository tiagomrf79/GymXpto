namespace Application.Features.Routines.Queries.GetRoutineListWithWorkouts;

public class RoutineWorkoutDto
{
    public Guid WorkoutId { get; set; }
    public Guid RoutineId { get; set; }
    public string Title { get; set; } = string.Empty;
}