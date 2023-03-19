using MediatR;

namespace Application.Features.ExerciseSets.Commands.DeleteCommand;

public class DeleteExerciseSetCommand : IRequest<DeleteExerciseSetCommandResponse>
{
    public Guid ExerciseSetId { get; set; }
}
