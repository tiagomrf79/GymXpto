namespace Application.Features.Workouts.Queries.GetRoutineWorkoutsList;

public class WorkoutListVm
{
    public Guid WorkoutId { get; set; }
    public Guid RoutineId { get; set; }
    public string Title { get; set; } = string.Empty;
}