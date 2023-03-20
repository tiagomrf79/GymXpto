namespace Application.Features.ExerciseSets.Commands.CreateExerciseSet;

public class CreateExerciseSetDto
{
    public Guid ExerciseSetId { get; set; }
    public Guid SupersetId { get; set; }
    public int Order { get; set; }
    public int TargetRepetitions { get; set; }
    public Guid ExerciseId { get; set; }
}