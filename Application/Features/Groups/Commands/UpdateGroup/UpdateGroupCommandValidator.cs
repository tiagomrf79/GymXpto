using Application.Interfaces.Persistence;
using FluentValidation;

namespace Application.Features.Groups.Commands.UpdateGroup;

public class UpdateGroupCommandValidator : AbstractValidator<UpdateGroupCommand>
{
    private readonly IWorkoutRepository _workoutRepository;

    public UpdateGroupCommandValidator(IWorkoutRepository workoutRepository)
    {
        _workoutRepository = workoutRepository;

        RuleFor(g => g.WorkoutId)
            .MustAsync(ExistsInWorkoutRepository).WithMessage("Invalid value for {PropertyName}.");

        RuleFor(g => g.Order)
            .NotNull()
            .WithMessage("{PropertyName} is required.")
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than zero.");

        RuleFor(g => g.RestBetweenSets)
            .NotNull()
            .WithMessage("{PropertyName} is required.")
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than zero.");
    }

    private async Task<bool> ExistsInWorkoutRepository(Guid workoutId, CancellationToken arg2)
    {
        var workoutFound = await _workoutRepository.GetByIdAsync(workoutId);
        return workoutFound != null;
    }
}