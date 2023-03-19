using Application.Responses;

namespace Application.Features.ExerciseSets.Commands.UpdateCommand;

public class UpdateExerciseSetCommandResponse : BaseResponse
{
    public UpdateExerciseSetDto ExerciseSet { get; set; } = default!;
}