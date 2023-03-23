using MediatR;

namespace Application.Features.Groups.Queries.GetGroupsFromWorkout;

public class GetGroupsFromWorkoutQuery : IRequest<GetGroupsFromWorkoutQueryResponse>
{
    public Guid WorkoutId { get; set; }
}
