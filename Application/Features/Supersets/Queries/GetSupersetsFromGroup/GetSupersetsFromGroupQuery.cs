using MediatR;

namespace Application.Features.Supersets.Queries.GetSupersetsFromGroup;

public class GetSupersetsFromGroupQuery : IRequest<GetSupersetsFromGroupQueryResponse>
{
    public Guid GroupId { get; set; }
}
