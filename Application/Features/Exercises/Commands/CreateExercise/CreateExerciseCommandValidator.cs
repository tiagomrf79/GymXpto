using Application.Interfaces.Persistence;
using Domain.Entities;
using FluentValidation;

namespace Application.Features.Exercises.Commands.CreateExercise;

public class CreateExerciseCommandValidator : AbstractValidator<CreateExerciseCommand>
{
    private readonly IAsyncRepository<Equipment> _equipmentRepository;

    public CreateExerciseCommandValidator(IAsyncRepository<Equipment> equipmentRepository)
    {
        _equipmentRepository = equipmentRepository;

        RuleFor(e => e.Name)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.");

        RuleFor(e => e.Instructions)
            .MaximumLength(500).When(e => !string.IsNullOrEmpty(e.Instructions)).WithMessage("{PropertyName} must not exceed 500 characters.");

        RuleFor(e => e.UtilityType)
            .IsInEnum().When(e => e.UtilityType.HasValue).WithMessage("Invalid value for {PropertyName}.");

        RuleFor(e => e.MechanicType)
            .IsInEnum().WithMessage("Invalid value for {PropertyName}.");

        RuleFor(e => e.MovementType)
            .IsInEnum().WithMessage("Invalid value for {PropertyName}.");

        RuleFor(e => e.MainEquipmentUsedId)
            .MustAsync(ExistsInEquipmentRepository).When(e => e.MainEquipmentUsedId.HasValue).WithMessage("Invalid value for {PropertyName}.");

        RuleFor(e => e.Comments)
            .MaximumLength(500).When(e => !string.IsNullOrEmpty(e.Comments)).WithMessage("{PropertyName} must not exceed 500 characters.");
    }

    private async Task<bool> ExistsInEquipmentRepository(Guid? equipmentId, CancellationToken arg2)
    {
        if (equipmentId == null) return true;

        var equipmentFound = await _equipmentRepository.GetByIdAsync(equipmentId.Value);
        return equipmentFound != null;
    }
}