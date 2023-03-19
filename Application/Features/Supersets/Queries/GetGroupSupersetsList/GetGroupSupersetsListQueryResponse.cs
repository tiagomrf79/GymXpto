using Application.Responses;

namespace Application.Features.Supersets.Queries.GetGroupSupersetsList;

public class GetGroupSupersetsListQueryResponse : BaseResponse
{
    public IList<SupersetListVm> SupersetsList { get; set; } = new List<SupersetListVm>();
}