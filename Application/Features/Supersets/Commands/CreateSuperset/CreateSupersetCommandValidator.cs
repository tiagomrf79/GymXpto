using Application.Interfaces.Persistence;
using FluentValidation;

namespace Application.Features.Supersets.Commands.CreateSuperset;

public class CreateSupersetCommandValidator : AbstractValidator<CreateSupersetCommand>
{
    private readonly IGroupRepository _groupRepository;

    public CreateSupersetCommandValidator(IGroupRepository groupRepository)
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