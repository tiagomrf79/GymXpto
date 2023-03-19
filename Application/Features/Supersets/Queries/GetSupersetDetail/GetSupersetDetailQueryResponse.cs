using Application.Responses;

namespace Application.Features.Supersets.Queries.GetSupersetDetail;

public class GetSupersetDetailQueryResponse : BaseResponse
{
    public SupersetDetailDto Superset { get; set; } = default!;
}