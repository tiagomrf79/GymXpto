using Application.Responses;

namespace Application.Features.Workouts.Queries.GetRoutineWorkoutsList;

public class GetRoutineWorkoutsListQueryResponse : BaseResponse
{
    public IList<WorkoutListVm> WorkoutsList { get; set; } = new List<WorkoutListVm>();

    public GetRoutineWorkoutsListQueryResponse() : base()
    {
    }
}