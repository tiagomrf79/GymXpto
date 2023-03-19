using FluentValidation;

namespace Application.Features.Supersets.Commands.UpdateCommand;

public class UpdateSupersetCommandValidator : AbstractValidator<UpdateSupersetCommand>
{
    public UpdateSupersetCommandValidator()
    {
        RuleFor(s => s.Order)
            .NotNull()
            .WithMessage("{PropertyName} is required.")
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than zero.");
    }
}