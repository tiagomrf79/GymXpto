﻿using Application.Interfaces.Persistence;
using Domain.Entities.Schedule;
using FluentValidation;

namespace Application.Features.Supersets.Commands.UpdateCommand;

public class UpdateSupersetCommandValidator : AbstractValidator<UpdateSupersetCommand>
{
    private readonly IAsyncRepository<Group> _groupRepository;

    public UpdateSupersetCommandValidator(IAsyncRepository<Group> groupRepository)
    {
        _groupRepository = groupRepository;

        RuleFor(s => s.GroupId)
            .MustAsync(ExistsInGroupRepository).WithMessage("Invalid value for {PropertyName}.");

        RuleFor(s => s.Order)
            .NotNull()
            .WithMessage("{PropertyName} is required.")
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than zero.");
    }

    private async Task<bool> ExistsInGroupRepository(Guid groupId, CancellationToken arg2)
    {
        var groupFound = await _groupRepository.GetByIdAsync(groupId);
        return groupFound != null;
    }
}