using MediatR;

namespace Application.Features.Groups.Queries.GetWorkoutGroupsList;

public class GetWorkoutGroupsListQuery : IRequest<GetWorkoutGroupsListQueryResponse>
{
    public Guid WorkoutId { get; set; }
}
