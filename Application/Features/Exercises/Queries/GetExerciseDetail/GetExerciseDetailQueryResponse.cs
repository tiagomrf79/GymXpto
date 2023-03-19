using Application.Responses;

namespace Application.Features.Exercises.Queries.GetExerciseDetail;

public class GetExerciseDetailQueryResponse : BaseResponse
{
    public ExerciseDetailDto Exercise { get; set; } = default!;
}