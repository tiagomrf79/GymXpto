using MediatR;

namespace Application.Features.Exercises.Commands.DeleteCommand;

public class DeleteExerciseCommand : IRequest<DeleteExerciseCommandResponse>
{
    public Guid ExerciseId { get; set; }
}
