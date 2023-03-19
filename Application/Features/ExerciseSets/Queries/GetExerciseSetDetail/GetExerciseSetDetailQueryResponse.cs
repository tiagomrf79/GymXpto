using Application.Responses;

namespace Application.Features.ExerciseSets.Queries.GetExerciseSetDetail;

public class GetExerciseSetDetailQueryResponse : BaseResponse
{
    public ExerciseSetDetailDto ExerciseSet { get; set; } = default!;
}