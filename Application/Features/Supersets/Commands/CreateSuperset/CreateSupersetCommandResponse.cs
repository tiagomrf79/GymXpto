using Application.Responses;

namespace Application.Features.Supersets.Commands.CreateSuperset;

public class CreateSupersetCommandResponse : BaseResponse
{
    public CreateSupersetDto Superset { get; set; } = default!;
}