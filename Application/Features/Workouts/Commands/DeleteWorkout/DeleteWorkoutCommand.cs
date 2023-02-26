using MediatR;

namespace Application.Features.Workouts.Commands.DeleteWorkout;

public class DeleteWorkoutCommand : IRequest<DeleteWorkoutCommandResponse>
{
    public Guid WorkoutId { get; set; }
}
