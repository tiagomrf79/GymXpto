using Application.Interfaces.Persistence;
using MediatR;

namespace Application.Features.ExerciseSets.Commands.DeleteExerciseSet;

public class DeleteExerciseSetCommandHandler : IRequestHandler<DeleteExerciseSetCommand, DeleteExerciseSetCommandResponse>
{
    private readonly IExerciseSetRepository _exerciseSetRepository;

    public DeleteExerciseSetCommandHandler(IExerciseSetRepository exerciseSetRepository)
    {
        _exerciseSetRepository = exerciseSetRepository;
    }

    public async Task<DeleteExerciseSetCommandResponse> Handle(DeleteExerciseSetCommand request, CancellationToken cancellationToken)
    {
        var exerciseSetToDelete = await _exerciseSetRepository.GetByIdAsync(request.ExerciseSetId);

        if (exerciseSetToDelete == null)
        {
            return new DeleteExerciseSetCommandResponse
            {
                Success = false,
                Message = "Exercise set not found."
            };
        }

        await _exerciseSetRepository.DeleteAsync(exerciseSetToDelete);

        return new DeleteExerciseSetCommandResponse
        {
            Success = true
        };
    }
}
