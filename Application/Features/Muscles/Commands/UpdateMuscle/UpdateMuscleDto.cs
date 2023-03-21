namespace Application.Features.Muscles.Commands.UpdateMuscle;

public class UpdateMuscleDto
{
    public Guid MuscleId { get; set; }
    public string Name { get; set; } = string.Empty;
}