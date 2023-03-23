using Application.Responses;

namespace Application.Features.Groups.Queries.GetGroupsFromWorkout;

public class GetGroupsFromWorkoutQueryResponse : BaseResponse
{
    public IList<GroupListVm> GroupsList { get; set; } = new List<GroupListVm>();
}