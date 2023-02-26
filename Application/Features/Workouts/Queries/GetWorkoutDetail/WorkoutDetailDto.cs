namespace Application.Features.Workouts.Queries.GetWorkoutDetail;

public class WorkoutDetailDto
{
    public Guid WorkoutId { get; set; }
    public Guid RoutineId { get; set; }
    public string Title { get; set; } = string.Empty;
}