using Application.Interfaces.Persistence;
using Domain.Entities;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Exercises.Commands.DeleteExercise;

public class DeleteExerciseCommandHandler : IRequestHandler<DeleteExerciseCommand, DeleteExerciseCommandResponse>
{
    private readonly IAsyncRepository<Exercise> _exerciseRepository;
    private readonly IAsyncRepository<ExerciseSet> _exerciseSetRepository;

    public DeleteExerciseCommandHandler(IAsyncRepository<Exercise> exerciseRepository, IAsyncRepository<ExerciseSet> exerciseSetRepository)
    {
        _exerciseRepository = exerciseRepository;
        _exerciseSetRepository = exerciseSetRepository;
    }

    public async Task<DeleteExerciseCommandResponse> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
    {
        var exerciseToDelete = await _exerciseRepository.GetByIdAsync(request.ExerciseId);

        if (exerciseToDelete == null)
        {
            return new DeleteExerciseCommandResponse
            {
                Success = false,
                Message = "Exercise not found."
            };
        }

        var linkedExerciseSets = (await _exerciseSetRepository.ListAllAsync()).Where(es => es.ExerciseId == request.ExerciseId);

        if (linkedExerciseSets.Count() > 0)
        {
            return new DeleteExerciseCommandResponse
            {
                Success = false,
                Message = "Exercise cannot be deleted since it's being used."
            };
        }

        await _exerciseRepository.DeleteAsync(exerciseToDelete);

        return new DeleteExerciseCommandResponse
        {
            Success = true,
        };
    }
}
