using MediatR;

namespace Application.Features.Workouts.Commands.UpdateWorkout;

public class UpdateWorkoutCommand : IRequest<UpdateWorkoutCommandResponse>
{
    public Guid WorkoutId { get; set; }
    public Guid RoutineId { get; set; }
    public string Title { get; set; } = string.Empty;
}
