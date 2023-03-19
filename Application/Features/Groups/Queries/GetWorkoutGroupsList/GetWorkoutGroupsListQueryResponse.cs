using Application.Responses;

namespace Application.Features.Groups.Queries.GetWorkoutGroupsList;

public class GetWorkoutGroupsListQueryResponse : BaseResponse
{
    public IList<GroupListVm> GroupsList { get; set; } = new List<GroupListVm>();
}