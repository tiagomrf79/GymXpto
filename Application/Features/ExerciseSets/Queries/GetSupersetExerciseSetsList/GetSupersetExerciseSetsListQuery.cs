using MediatR;

namespace Application.Features.ExerciseSets.Queries.GetSupersetExerciseSetsList;

public class GetSupersetExerciseSetsListQuery : IRequest<GetSupersetExerciseSetsListQueryResponse>
{
    public Guid SupersetId { get; set; }
}
