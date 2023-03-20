using MediatR;

namespace Application.Features.Groups.Commands.DeleteGroup;

public class DeleteGroupCommand : IRequest<DeleteGroupCommandResponse>
{
    public Guid GroupId { get; set; }
}
