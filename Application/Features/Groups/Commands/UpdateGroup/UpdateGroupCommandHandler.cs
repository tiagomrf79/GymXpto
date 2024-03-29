﻿using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Groups.Commands.UpdateGroup;

public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, UpdateGroupCommandResponse>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IWorkoutRepository _workoutRepository;
    private readonly IMapper _mapper;

    public UpdateGroupCommandHandler(IGroupRepository groupRepository, IWorkoutRepository workoutRepository, IMapper mapper)
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

        var validator = new UpdateGroupCommandValidator(_workoutRepository);
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
