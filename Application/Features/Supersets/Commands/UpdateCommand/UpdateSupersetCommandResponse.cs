using Application.Responses;

namespace Application.Features.Supersets.Commands.UpdateCommand;

public class UpdateSupersetCommandResponse : BaseResponse
{
    public UpdateSupersetDto Superset { get; set; } = default!;
}