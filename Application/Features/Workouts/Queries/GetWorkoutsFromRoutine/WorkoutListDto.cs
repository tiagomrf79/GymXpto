namespace Application.Features.Workouts.Queries.GetWorkoutsFromRoutine;

public class WorkoutListDto
{
    public Guid WorkoutId { get; set; }
    public Guid RoutineId { get; set; }
    public WorkoutListRoutineDto Routine { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
}