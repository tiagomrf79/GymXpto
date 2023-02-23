using MediatR;

namespace Application.Features.Routines.Commands.UpdateRoutine;

public class UpdateRoutineCommand : IRequest<UpdateRoutineCommandResponse>
{
    public Guid RoutineId { get; set; }
    
    //TODO: Add UserId
    //public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

}
