using MediatR;

namespace Application.Features.Groups.Commands.DeleteCommand;

public class DeleteGroupCommand : IRequest<DeleteGroupCommandResponse>
{
    public Guid GroupId { get; set; }
}
