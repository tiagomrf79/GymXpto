using Application.Responses;

namespace Application.Features.Workouts.Commands.UpdateWorkout;

public class UpdateWorkoutCommandResponse : BaseResponse
{
    public UpdateWorkoutDto Workout { get; set; } = default!;

    public UpdateWorkoutCommandResponse() : base()
    {
    }
}