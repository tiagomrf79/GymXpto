using FluentValidation;

namespace Application.Features.Routines.Commands.UpdateRoutine;

public class UpdateRoutineCommandValidator : AbstractValidator<UpdateRoutineCommand>
{
	public UpdateRoutineCommandValidator()
	{
        RuleFor(v => v.Title)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.");
    }
}
