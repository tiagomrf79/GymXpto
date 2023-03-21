using MediatR;

namespace Application.Features.Muscles.Commands.DeleteMuscle;

public class DeleteMuscleCommand : IRequest<DeleteMuscleCommandResponse>
{
    public Guid MuscleId { get; set; }
}
