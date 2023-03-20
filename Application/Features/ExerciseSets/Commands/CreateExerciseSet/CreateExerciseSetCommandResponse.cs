using Application.Responses;

namespace Application.Features.ExerciseSets.Commands.CreateExerciseSet;

public class CreateExerciseSetCommandResponse : BaseResponse
{
    public CreateExerciseSetDto ExerciseSet { get; set; } = default!;
}