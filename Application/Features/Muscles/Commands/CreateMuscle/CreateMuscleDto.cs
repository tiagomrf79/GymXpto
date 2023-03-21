namespace Application.Features.Muscles.Commands.CreateMuscle;

public class CreateMuscleDto
{
    public Guid MuscleId { get; set; }
    public string Name { get; set; } = string.Empty;
}