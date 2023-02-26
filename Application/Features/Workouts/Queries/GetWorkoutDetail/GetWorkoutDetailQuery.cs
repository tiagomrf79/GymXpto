using MediatR;

namespace Application.Features.Workouts.Queries.GetWorkoutDetail;

public class GetWorkoutDetailQuery : IRequest<GetWorkoutDetailQueryResponse>
{
    public Guid WorkoutId { get; set; }
}
