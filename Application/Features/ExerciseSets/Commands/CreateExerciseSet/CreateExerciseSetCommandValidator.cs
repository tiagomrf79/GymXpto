using Application.Interfaces.Persistence;
using Domain.Entities;
using Domain.Entities.Schedule;
using FluentValidation;

namespace Application.Features.ExerciseSets.Commands.CreateExerciseSet;

public class CreateExerciseSetCommandValidator : AbstractValidator<CreateExerciseSetCommand>
{
    private readonly IAsyncRepository<Superset> _supersetRepository;
    private readonly IAsyncRepository<Exercise> _exerciseRepository;

    public CreateExerciseSetCommandValidator(IAsyncRepository<Superset> supersetRepository, IAsyncRepository<Exercise> exerciseRepository)
    {
        _supersetRepository = supersetRepository;
        _exerciseRepository = exerciseRepository;

        RuleFor(e => e.SupersetId)
            .MustAsync(ExistsInSupersetRepository).WithMessage("Invalid value for {PropertyName}.");

        RuleFor(e => e.Order)
            .NotNull()
            .WithMessage("{PropertyName} is required.")
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than zero.");

        RuleFor(e => e.TargetRepetitions)
            .NotNull()
            .WithMessage("{PropertyName} is required.")
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than zero.");

        RuleFor(e => e.ExerciseId)
            .MustAsync(ExistsInExerciseRepository).WithMessage("Invalid value for {PropertyName}.");
    }

    private async Task<bool> ExistsInSupersetRepository(Guid SupersetId, CancellationToken arg2)
    {
        var supersetFound = await _supersetRepository.GetByIdAsync(SupersetId);
        return supersetFound != null;
    }

    private async Task<bool> ExistsInExerciseRepository(Guid ExerciseId, CancellationToken arg2)
    {
        var exerciseFound = await _exerciseRepository.GetByIdAsync(ExerciseId);
        return exerciseFound != null;
    }
}