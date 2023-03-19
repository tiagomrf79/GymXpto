using Application.Interfaces.Persistence;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Workouts.Commands.DeleteWorkout;

public class DeleteWorkoutCommandHandler : IRequestHandler<DeleteWorkoutCommand, DeleteWorkoutCommandResponse>
{
    private readonly IAsyncRepository<Workout> _workoutRepository;

    public DeleteWorkoutCommandHandler(IAsyncRepository<Workout> workoutRepository)
    {
        _workoutRepository = workoutRepository;
    }

    public async Task<DeleteWorkoutCommandResponse> Handle(DeleteWorkoutCommand request, CancellationToken cancellationToken)
    {
        var workoutToDelete = await _workoutRepository.GetByIdAsync(request.WorkoutId);

        if (workoutToDelete == null)
        {
            return new DeleteWorkoutCommandResponse
            {
                Success = false,
                Message = "Workout not found."
            };
        }

        await _workoutRepository.DeleteAsync(workoutToDelete);

        return new DeleteWorkoutCommandResponse
        {
            Success = true,
        };
    }
}
