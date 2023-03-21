using FluentValidation;

namespace Application.Features.Muscles.Commands.UpdateMuscle;

public class UpdateMuscleCommandValidator : AbstractValidator<UpdateMuscleCommand>
{
    public UpdateMuscleCommandValidator()
    {
        RuleFor(e => e.Name)
        .NotNull()
        .NotEmpty().WithMessage("{PropertyName} is required.")
        .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.");
    }
}