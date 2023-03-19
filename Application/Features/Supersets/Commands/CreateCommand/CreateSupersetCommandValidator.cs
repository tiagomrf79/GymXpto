using FluentValidation;

namespace Application.Features.Supersets.Commands.CreateCommand;

public class CreateSupersetCommandValidator : AbstractValidator<CreateSupersetCommand>
{
    public CreateSupersetCommandValidator()
    {
        RuleFor(s => s.Order)
            .NotNull()
            .WithMessage("{PropertyName} is required.")
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than zero.");
    }
}