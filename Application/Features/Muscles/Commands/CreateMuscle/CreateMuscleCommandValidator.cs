using FluentValidation;

namespace Application.Features.Muscles.Commands.CreateMuscle;

public class CreateMuscleCommandValidator : AbstractValidator<CreateMuscleCommand>
{
    public CreateMuscleCommandValidator()
    {
        RuleFor(e => e.Name)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.");
    }
}