using Application.Responses;

namespace Application.Features.Groups.Commands.CreateCommand;

public class CreateGroupCommandResponse : BaseResponse
{
    public CreateGroupDto Group { get; set; } = default!;
}