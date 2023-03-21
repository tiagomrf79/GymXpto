using Application.Responses;

namespace Application.Features.Muscles.Queries.GetMuscleList;

public class GetMuscleListQueryResponse : BaseResponse
{
    public IList<MuscleListVm> Muscle { get; set; } = new List<MuscleListVm>();
}