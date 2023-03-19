using Application.Responses;

namespace Application.Features.Exercises.Commands.CreateCommand;

public class CreateExerciseCommandResponse : BaseResponse
{
    public CreateExerciseDto Exercise { get; set; } = default!;
}