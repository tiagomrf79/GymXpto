using MediatR;

namespace Application.Features.ExerciseSets.Commands.UpdateExerciseSet;

public class UpdateExerciseSetCommand : IRequest<UpdateExerciseSetCommandResponse>
{
    public Guid ExerciseSetId { get; set; }
    public Guid SupersetId { get; set; }
    public int Order { get; set; }
    public int TargetRepetitions { get; set; }
    public Guid ExerciseId { get; set; }
}
