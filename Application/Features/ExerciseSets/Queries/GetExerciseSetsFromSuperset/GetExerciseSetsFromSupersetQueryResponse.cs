using Application.Responses;

namespace Application.Features.ExerciseSets.Queries.GetExerciseSetsFromSuperset;

public class GetExerciseSetsFromSupersetQueryResponse : BaseResponse
{
    public IList<ExerciseSetListVm> ExerciseSetList { get; set; } = new List<ExerciseSetListVm>();
}