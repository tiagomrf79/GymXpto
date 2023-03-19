using Application.Interfaces.Persistence;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Groups.Commands.DeleteCommand;

public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, DeleteGroupCommandResponse>
{
    private readonly IAsyncRepository<Group> _groupRepository;

    public DeleteGroupCommandHandler(IAsyncRepository<Group> groupRepository)
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
