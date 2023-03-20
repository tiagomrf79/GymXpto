using Application.Responses;

namespace Application.Features.Exercises.Commands.UpdateExercise;

public class UpdateExerciseCommandResponse : BaseResponse
{
    public UpdateExerciseDto Exercise { get; set; } = default!;
}