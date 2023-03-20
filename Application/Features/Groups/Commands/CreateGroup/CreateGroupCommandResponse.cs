using Application.Responses;

namespace Application.Features.Groups.Commands.CreateGroup;

public class CreateGroupCommandResponse : BaseResponse
{
    public CreateGroupDto Group { get; set; } = default!;
}