namespace Application.Features.Workouts.Queries.GetWorkoutDetail;

public class WorkoutDetailDto
{
    public Guid WorkoutId { get; set; }
    public Guid RoutineId { get; set; }
    public WorkoutDetailRoutineDto Routine { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
}