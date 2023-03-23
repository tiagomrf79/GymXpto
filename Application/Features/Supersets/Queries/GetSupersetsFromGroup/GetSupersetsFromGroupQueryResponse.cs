using Application.Responses;

namespace Application.Features.Supersets.Queries.GetSupersetsFromGroup;

public class GetSupersetsFromGroupQueryResponse : BaseResponse
{
    public IList<SupersetListVm> SupersetsList { get; set; } = new List<SupersetListVm>();
}