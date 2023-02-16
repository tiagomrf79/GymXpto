using MediatR;

namespace Application.Features.Routines.Commands.DeleteRoutine;

public class DeleteRoutineCommand : IRequest<DeleteRoutineCommandResponse>
{
    public Guid RoutineId { get; set; }
}
