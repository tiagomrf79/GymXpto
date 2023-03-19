using MediatR;

namespace Application.Features.Groups.Queries.GetGroupDetail;

public class GetGroupDetailQuery : IRequest<GetGroupDetailQueryResponse>
{
    public Guid GroupId { get; set; }
}
