using MediatR;

namespace Application.Features.ExerciseSets.Queries.GetExerciseSetsFromSuperset;

public class GetExerciseSetsFromSupersetQuery : IRequest<GetExerciseSetsFromSupersetQueryResponse>
{
    public Guid SupersetId { get; set; }
}
