using MediatR;

namespace Application.Features.ExerciseSets.Commands.DeleteExerciseSet;

public class DeleteExerciseSetCommand : IRequest<DeleteExerciseSetCommandResponse>
{
    public Guid ExerciseSetId { get; set; }
}
