using MediatR;

namespace Application.Features.Workouts.Commands.CreateWorkout;

public class CreateWorkoutCommand : IRequest<CreateWorkoutCommandResponse>
{
    public Guid RoutineId { get; set; }
    public string Title { get; set; } = string.Empty;
}
