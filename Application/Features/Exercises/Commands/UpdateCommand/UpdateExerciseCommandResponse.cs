using Application.Responses;

namespace Application.Features.Exercises.Commands.UpdateCommand;

public class UpdateExerciseCommandResponse : BaseResponse
{
    public UpdateExerciseDto Exercise { get; set; } = default!;
}