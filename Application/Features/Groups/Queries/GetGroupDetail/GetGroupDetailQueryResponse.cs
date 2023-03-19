using Application.Responses;

namespace Application.Features.Groups.Queries.GetGroupDetail;

public class GetGroupDetailQueryResponse : BaseResponse
{
    public GroupDetailDto Group { get; set; } = default!;
}