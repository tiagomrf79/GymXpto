using Application.Responses;

namespace Application.Features.Routines.Queries.GetRoutinesListWithWorkouts;

public class GetRoutinesListWithWorkoutsQueryResponse : BaseResponse
{
    public List<RoutineWorkoutsListVm> RoutineWorkoutsList { get; set; } = new List<RoutineWorkoutsListVm>();
}