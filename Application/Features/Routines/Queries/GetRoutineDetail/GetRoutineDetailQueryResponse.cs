using Application.Responses;

namespace Application.Features.Routines.Queries.GetRoutineDetail;

public class GetRoutineDetailQueryResponse : BaseResponse
{
    public RoutineDetailDto Routine { get; set; } = default!;
}
