using Application.Responses;

namespace Application.Features.Supersets.Commands.UpdateSuperset;

public class UpdateSupersetCommandResponse : BaseResponse
{
    public UpdateSupersetDto Superset { get; set; } = default!;
}