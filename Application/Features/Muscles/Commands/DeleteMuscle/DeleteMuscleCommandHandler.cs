using Application.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Features.Muscles.Commands.DeleteMuscle;

public class DeleteMuscleCommandHandler : IRequestHandler<DeleteMuscleCommand, DeleteMuscleCommandResponse>
{
    private readonly IAsyncRepository<Muscle> _muscleRepository;
    private readonly IAsyncRepository<Exercise> _exerciseRepository;

    public DeleteMuscleCommandHandler(IAsyncRepository<Muscle> muscleRepository, IAsyncRepository<Exercise> exerciseRepository)
    {
        _muscleRepository = muscleRepository;
        _exerciseRepository = exerciseRepository;
    }

    public async Task<DeleteMuscleCommandResponse> Handle(DeleteMuscleCommand request, CancellationToken cancellationToken)
    {
        var muscleToDelete = await _muscleRepository.GetByIdAsync(request.MuscleId);

        if (muscleToDelete == null)
        {
            return new DeleteMuscleCommandResponse
            {
                Success = false,
                Message = "Muscle not found."
            };
        }

        var linkedExercises = (await _exerciseRepository.ListAllAsync()).Where(e => e.MusclesWorked.Where(m => m.MuscleId == request.MuscleId).Any());

        if (linkedExercises.Count() > 0)
        {
            return new DeleteMuscleCommandResponse
            {
                Success = false,
                Message = "Muscle cannot be deleted since it's being used."
            };
        }

        await _muscleRepository.DeleteAsync(muscleToDelete);

        return new DeleteMuscleCommandResponse
        {
            Success = true
        };
    }
}
