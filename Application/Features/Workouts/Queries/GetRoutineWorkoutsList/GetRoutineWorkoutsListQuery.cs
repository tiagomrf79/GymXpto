using MediatR;

namespace Application.Features.Workouts.Queries.GetRoutineWorkoutsList;

public class GetRoutineWorkoutsListQuery : IRequest<GetRoutineWorkoutsListQueryResponse>
{
    public Guid RoutineId { get; set; }
}
