using MediatR;

namespace Application.Features.Exercises.Queries.GetExerciseDetail;

public class GetExerciseDetailQuery : IRequest<GetExerciseDetailQueryResponse>
{
    public Guid ExerciseId { get; set; }
}
