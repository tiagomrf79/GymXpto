using Domain.Entities.Schedule;

namespace Application.Features.Routines.Queries.GetRoutineListWithWorkouts;

public class RoutineWorkoutsListDto
{
    public Guid RoutineId { get; set; }
    public string Title { get; set; } = string.Empty;
    public IList<RoutineWorkoutDto>? Workouts { get; private set; } = new List<RoutineWorkoutDto>();
}