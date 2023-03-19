using MediatR;

namespace Application.Features.Supersets.Queries.GetGroupSupersetsList;

public class GetGroupSupersetsListQuery : IRequest<GetGroupSupersetsListQueryResponse>
{
    public Guid GroupId { get; set; }
}
