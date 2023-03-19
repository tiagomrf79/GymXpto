using Application.Responses;

namespace Application.Features.Supersets.Commands.CreateCommand;

public class CreateSupersetCommandResponse : BaseResponse
{
    public CreateSupersetDto Superset { get; set; } = default!;
}