using FluentValidation;

namespace Application.Features.Routines.Commands.CreateRoutine;

public class CreateRoutineCommandValidator : AbstractValidator<CreateRoutineCommand>
{
	public CreateRoutineCommandValidator()
	{
		RuleFor(v => v.Title)
			.NotNull()
			.NotEmpty().WithMessage("{PropertyName} is required.")
			.MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.");
    }
}
