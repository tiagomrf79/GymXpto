using MediatR;

namespace Application.Features.Exercises.Commands.DeleteExercise;

public class DeleteExerciseCommand : IRequest<DeleteExerciseCommandResponse>
{
    public Guid ExerciseId { get; set; }
}
