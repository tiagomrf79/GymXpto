using MediatR;

namespace Application.Features.Workouts.Queries.GetWorkoutsFromRoutine;

public class GetWorkoutsFromRoutineQuery : IRequest<GetWorkoutsFromRoutineQueryResponse>
{
    public Guid RoutineId { get; set; }
}
