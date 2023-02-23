namespace Application.Features.Routines.Queries.GetRoutinesList;

public class RoutineListVm
{
    public Guid RoutineId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}