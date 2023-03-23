using Application.Interfaces.Persistence;
using MediatR;

namespace Application.Features.Groups.Commands.DeleteGroup;

public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, DeleteGroupCommandResponse>
{
    private readonly IGroupRepository _groupRepository;

    public DeleteGroupCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<DeleteGroupCommandResponse> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var groupToDelete = await _groupRepository.GetByIdAsync(request.GroupId);

        if (groupToDelete == null)
        {
            return new DeleteGroupCommandResponse
            {
                Success = false,
                Message = "Group not found."
            };
        }

        await _groupRepository.DeleteAsync(groupToDelete);

        return new DeleteGroupCommandResponse
        {
            Success = true
        };
    }
}
