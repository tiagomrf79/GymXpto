using Domain.Entities.Schedule;

namespace Application.Features.Routines.Queries.GetRoutinesListWithWorkouts;

public class RoutineWorkoutsListVm
{
    public Guid RoutineId { get; set; }
    public string Title { get; set; } = string.Empty;
    public IList<RoutineWorkoutDto>? Workouts { get; private set; } = new List<RoutineWorkoutDto>();
}