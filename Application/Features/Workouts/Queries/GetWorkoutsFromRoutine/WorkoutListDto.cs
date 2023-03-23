namespace Application.Features.Workouts.Queries.GetWorkoutsFromRoutine;

public class WorkoutListDto
{
    public Guid WorkoutId { get; set; }
    public Guid RoutineId { get; set; }
    public string Title { get; set; } = string.Empty;
}