using MediatR;

namespace Application.Features.Muscles.Commands.CreateMuscle;

public class CreateMuscleCommand : IRequest<CreateMuscleCommandResponse>
{
    public string Name { get; set; } = string.Empty;
}
