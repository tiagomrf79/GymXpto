using Application.Interfaces.Persistence;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.ExerciseSets.Commands.DeleteCommand;

public class DeleteExerciseSetCommandHandler : IRequestHandler<DeleteExerciseSetCommand, DeleteExerciseSetCommandResponse>
{
    private readonly IAsyncRepository<ExerciseSet> _exerciseSetRepository;

    public DeleteExerciseSetCommandHandler(IAsyncRepository<ExerciseSet> exerciseSetRepository)
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
