namespace BlazorWasm.ViewModels;

public class RoutineViewModel
{
    public Guid RoutineId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}
