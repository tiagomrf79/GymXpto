using Application.Responses;

namespace Application.Features.Groups.Commands.UpdateGroup;

public class UpdateGroupCommandResponse : BaseResponse
{
    public UpdateGroupDto Group { get; set; } = default!;
}