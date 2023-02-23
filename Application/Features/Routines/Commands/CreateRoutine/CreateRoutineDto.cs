namespace Application.Features.Routines.Commands.CreateRoutine;

public class CreateRoutineDto
{
    public Guid RoutineId { get; set; }
    
    //TODO: Add UserId
    //public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}
