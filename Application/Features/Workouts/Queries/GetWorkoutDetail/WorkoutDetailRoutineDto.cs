namespace Application.Features.Workouts.Queries.GetWorkoutDetail;

public class WorkoutDetailRoutineDto
{
    public Guid RoutineId { get; set; }
    public string Title { get; set; } = string.Empty;
}