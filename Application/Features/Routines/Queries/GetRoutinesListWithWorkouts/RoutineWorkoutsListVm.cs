namespace Application.Features.Routines.Queries.GetRoutinesListWithWorkouts;

public class RoutineWorkoutsListVm
{
    public Guid RoutineId { get; set; }
    public string Title { get; set; } = string.Empty;
    public ICollection<RoutineWorkoutDto>? Workouts { get; set; }
}