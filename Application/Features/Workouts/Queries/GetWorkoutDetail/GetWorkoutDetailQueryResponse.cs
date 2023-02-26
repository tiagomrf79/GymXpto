using Application.Responses;

namespace Application.Features.Workouts.Queries.GetWorkoutDetail;

public class GetWorkoutDetailQueryResponse : BaseResponse
{
    public WorkoutDetailDto Workout { get; set; } = default!;

    public GetWorkoutDetailQueryResponse() : base()
    {
    }
}