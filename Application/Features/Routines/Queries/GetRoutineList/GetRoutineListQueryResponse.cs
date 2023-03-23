using Application.Responses;

namespace Application.Features.Routines.Queries.GetRoutineList;

public class GetRoutineListQueryResponse : BaseResponse
{
    public IList<RoutineListDto> RoutineList { get; set; } = new List<RoutineListDto>();
}