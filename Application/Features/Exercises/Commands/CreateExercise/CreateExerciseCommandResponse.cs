using Application.Responses;

namespace Application.Features.Exercises.Commands.CreateExercise;

public class CreateExerciseCommandResponse : BaseResponse
{
    public CreateExerciseDto Exercise { get; set; } = default!;
}