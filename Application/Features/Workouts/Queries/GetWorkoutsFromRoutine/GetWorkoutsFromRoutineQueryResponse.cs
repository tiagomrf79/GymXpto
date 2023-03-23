using Application.Responses;

namespace Application.Features.Workouts.Queries.GetWorkoutsFromRoutine;

public class GetWorkoutsFromRoutineQueryResponse : BaseResponse
{
    public IList<WorkoutListDto> WorkoutList { get; set; } = new List<WorkoutListDto>();
}