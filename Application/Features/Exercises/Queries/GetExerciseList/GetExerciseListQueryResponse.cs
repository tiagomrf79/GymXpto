using Application.Responses;

namespace Application.Features.Exercises.Queries.GetExerciseList;

public class GetExerciseListQueryResponse : BaseResponse
{
    public IList<ExerciseListVm> ExerciseList { get; set; } = new List<ExerciseListVm>();
}