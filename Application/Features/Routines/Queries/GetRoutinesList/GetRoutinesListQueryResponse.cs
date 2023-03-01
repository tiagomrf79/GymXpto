using Application.Responses;

namespace Application.Features.Routines.Queries.GetRoutinesList;

public class GetRoutinesListQueryResponse : BaseResponse
{
    public IList<RoutineListVm> RoutinesList { get; set; } = new List<RoutineListVm>();

    public GetRoutinesListQueryResponse() : base()
    {
    }
}