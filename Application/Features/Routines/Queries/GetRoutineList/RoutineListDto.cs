namespace Application.Features.Routines.Queries.GetRoutineList;

public class RoutineListDto
{
    public Guid RoutineId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}