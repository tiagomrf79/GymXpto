using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Exercises.Queries.GetExerciseList;

public class GetExerciseListQueryHandler : IRequestHandler<GetExerciseListQuery, GetExerciseListQueryResponse>
{
    private readonly IAsyncRepository<Exercise> _exerciseRepository;
    private readonly IMapper _mapper;

    public GetExerciseListQueryHandler(IAsyncRepository<Exercise> exerciseRepository, IMapper mapper)
    {
        _exerciseRepository = exerciseRepository;
        _mapper = mapper;
    }

    public async Task<GetExerciseListQueryResponse> Handle(GetExerciseListQuery request, CancellationToken cancellationToken)
    {
        var exerciseList = (await _exerciseRepository.ListAllAsync()).OrderBy(e => e.Name);

        return new GetExerciseListQueryResponse
        {
            Success = true,
            ExerciseList = _mapper.Map<List<ExerciseListVm>>(exerciseList)
        };
    }
}
