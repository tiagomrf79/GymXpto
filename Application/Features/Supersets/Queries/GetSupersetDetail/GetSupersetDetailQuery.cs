using MediatR;

namespace Application.Features.Supersets.Queries.GetSupersetDetail;

public class GetSupersetDetailQuery : IRequest<GetSupersetDetailQueryResponse>
{
    public Guid SupersetId { get; set; }
}
