using MediatR;

namespace Application.Features.Muscles.Commands.UpdateMuscle;

public class UpdateMuscleCommand : IRequest<UpdateMuscleCommandResponse>
{
    public Guid MuscleId { get; set; }
    public string Name { get; set; } = string.Empty;
}
