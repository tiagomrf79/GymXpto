using Application.Interfaces.Persistence;
using FluentValidation;

namespace Application.Features.Workouts.Commands.CreateWorkout;

public class CreateWorkoutCommandValidator : AbstractValidator<CreateWorkoutCommand>
{
    private readonly IRoutineRepository _routineRepository;

    public CreateWorkoutCommandValidator(IRoutineRepository routineRepository)
    {
        _routineRepository = routineRepository;

        RuleFor(w => w.RoutineId)
            .MustAsync(ExistsInRoutineRepository).WithMessage("Invalid value for {PropertyName}.");

        RuleFor(w => w.Title)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.");
    }

    private async Task<bool> ExistsInRoutineRepository(Guid routineId, CancellationToken arg2)
    {
        var routineFound = await _routineRepository.GetByIdAsync(routineId);
        return routineFound != null;
    }
}