using Application.Responses;

namespace Application.Features.ExerciseSets.Commands.CreateCommand;

public class CreateExerciseSetCommandResponse : BaseResponse
{
    public CreateExerciseSetDto ExerciseSet { get; set; } = default!;
}