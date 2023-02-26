using FluentValidation;

namespace Application.Features.Workouts.Commands.UpdateWorkout;

public class UpdateWorkoutCommandValidator : AbstractValidator<UpdateWorkoutCommand>
{
    public UpdateWorkoutCommandValidator()
    {
        RuleFor(w => w.Title)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.");
    }
}