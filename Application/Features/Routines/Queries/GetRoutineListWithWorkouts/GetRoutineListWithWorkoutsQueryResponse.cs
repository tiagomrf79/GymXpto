using Application.Responses;

namespace Application.Features.Routines.Queries.GetRoutineListWithWorkouts;

public class GetRoutineListWithWorkoutsQueryResponse : BaseResponse
{
    public List<RoutineWorkoutsListDto> RoutineWorkoutsList { get; set; } = new List<RoutineWorkoutsListDto>();
}