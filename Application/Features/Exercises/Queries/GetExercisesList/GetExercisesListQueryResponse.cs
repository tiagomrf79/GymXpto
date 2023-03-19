using Application.Responses;

namespace Application.Features.Exercises.Queries.GetExercisesList;

public class GetExercisesListQueryResponse : BaseResponse
{
    public IList<ExerciseListVm> ExerciseList { get; set; } = new List<ExerciseListVm>();
}