using Application.Responses;

namespace Application.Features.ExerciseSets.Queries.GetSupersetExerciseSetsList;

public class GetSupersetExerciseSetsListQueryResponse : BaseResponse
{
    public IList<ExerciseSetListVm> ExerciseSetList { get; set; } = new List<ExerciseSetListVm>();
}