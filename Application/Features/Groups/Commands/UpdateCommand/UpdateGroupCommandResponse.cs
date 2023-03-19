using Application.Responses;

namespace Application.Features.Groups.Commands.UpdateCommand;

public class UpdateGroupCommandResponse : BaseResponse
{
    public UpdateGroupDto Group { get; set; } = default!;
}