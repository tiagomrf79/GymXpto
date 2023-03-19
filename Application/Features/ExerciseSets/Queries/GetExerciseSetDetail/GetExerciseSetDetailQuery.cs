using MediatR;

namespace Application.Features.ExerciseSets.Queries.GetExerciseSetDetail;

public class GetExerciseSetDetailQuery : IRequest<GetExerciseSetDetailQueryResponse>
{
    public Guid ExerciseSetId { get; set; }
}
