using MediatR;

namespace Application.Features.Routines.Commands.DeleteRoutine;

public class DeleteRoutineCommand : IRequest
{
    public Guid RoutineId { get; set; }
}
