using MediatR;

namespace Application.Features.Routines.Queries.GetRoutineDetail;

public class GetRoutineDetailQuery : IRequest<GetRoutineDetailQueryResponse>
{
    public Guid RoutineId { get; set; }
}
