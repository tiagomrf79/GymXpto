using Application.Responses;

namespace Application.Features.Routines.Queries.GetRoutinesList;

public class GetRoutinesListQueryResponse : BaseResponse
{
    public IList<RoutineListVm> RoutineList { get; set; } = new List<RoutineListVm>();
}