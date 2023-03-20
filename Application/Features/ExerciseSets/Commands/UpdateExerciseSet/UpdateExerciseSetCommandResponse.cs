using Application.Responses;

namespace Application.Features.ExerciseSets.Commands.UpdateExerciseSet;

public class UpdateExerciseSetCommandResponse : BaseResponse
{
    public UpdateExerciseSetDto ExerciseSet { get; set; } = default!;
}