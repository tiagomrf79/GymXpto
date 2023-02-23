namespace Application.Features.Routines.Queries.GetRoutineDetail;

public class RoutineDetailDto
{
    public Guid RoutineId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}