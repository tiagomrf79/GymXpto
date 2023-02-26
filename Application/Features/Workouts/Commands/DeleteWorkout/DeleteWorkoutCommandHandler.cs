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
        var commandResponse = new DeleteWorkoutCommandResponse();
        var entityToDelete = await _workoutRepository.GetByIdAsync(request.WorkoutId);

        if (entityToDelete == null)
        {
            commandResponse.Success = false;
            commandResponse.Message = "Workout not found.";
        }

        if (commandResponse.Success)
        {
            await _workoutRepository.DeleteAsync(entityToDelete!);
        }

        return commandResponse;
    }
}
