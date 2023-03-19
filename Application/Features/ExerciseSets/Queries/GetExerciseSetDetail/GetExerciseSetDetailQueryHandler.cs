using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.ExerciseSets.Queries.GetExerciseSetDetail;

public class GetExerciseSetDetailQueryHandler : IRequestHandler<GetExerciseSetDetailQuery, GetExerciseSetDetailQueryResponse>
{
    private readonly IAsyncRepository<ExerciseSet> _exerciseSetRepository;
    private readonly IMapper _mapper;

    public GetExerciseSetDetailQueryHandler(IAsyncRepository<ExerciseSet> exerciseSetRepository, IMapper mapper)
    {
        _exerciseSetRepository = exerciseSetRepository;
        _mapper = mapper;
    }

    public async Task<GetExerciseSetDetailQueryResponse> Handle(GetExerciseSetDetailQuery request, CancellationToken cancellationToken)
    {
        var exerciseSetFound = await _exerciseSetRepository.GetByIdAsync(request.ExerciseSetId);

        if (exerciseSetFound == null)
        {
            return new GetExerciseSetDetailQueryResponse
            {
                Success = false,
                Message = "Exercise set not found."
            };
        }

        return new GetExerciseSetDetailQueryResponse
        {
            Success = true,
            ExerciseSet = _mapper.Map<ExerciseSetDetailDto>(exerciseSetFound)
        };
    }
}
