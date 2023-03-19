using Application.Responses;

namespace Application.Features.Workouts.Commands.CreateWorkout;

public class CreateWorkoutCommandResponse : BaseResponse
{
    public CreateWorkoutDto Workout { get; set; } = default!;
}