namespace Application.Features.Workouts.Queries.GetWorkoutsFromRoutine;

public class WorkoutListRoutineDto
{
    public Guid RoutineId { get; set; }
    public string Title { get; set; } = string.Empty;
}