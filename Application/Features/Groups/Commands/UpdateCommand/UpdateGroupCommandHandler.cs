using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Groups.Commands.UpdateCommand;

public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, UpdateGroupCommandResponse>
{
    private readonly IAsyncRepository<Group> _groupRepository;
    private readonly IAsyncRepository<Workout> _workoutRepository;
    private readonly IMapper _mapper;

    public UpdateGroupCommandHandler(IAsyncRepository<Group> groupRepository, IAsyncRepository<Workout> workoutRepository, IMapper mapper)
    {
        _groupRepository = groupRepository;
        _workoutRepository = workoutRepository;
        _mapper = mapper;
    }
    public async Task<UpdateGroupCommandResponse> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var groupToUpdate = await _groupRepository.GetByIdAsync(request.GroupId);

        if (groupToUpdate == null)
        {
            return new UpdateGroupCommandResponse
            {
                Success = false,
                Message = "Group not found."
            };
        };

        var workoutFound = await _workoutRepository.GetByIdAsync(request.WorkoutId);

        if (workoutFound == null)
        {
            return new UpdateGroupCommandResponse
            {
                Success = false,
                Message = "Workout not found."
            };
        }

        var validator = new UpdateGroupCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return new UpdateGroupCommandResponse
            {
                Success = false,
                ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }

        _mapper.Map(request, groupToUpdate, typeof(UpdateGroupCommand), typeof(Group));
        await _groupRepository.UpdateAsync(groupToUpdate);

        return new UpdateGroupCommandResponse
        {
            Success = true,
            Group = _mapper.Map<UpdateGroupDto>(groupToUpdate)
        };
    }
}
