using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Exercises.Queries.GetExercisesList;

public class GetExercisesListQueryHandler : IRequestHandler<GetExercisesListQuery, GetExercisesListQueryResponse>
{
    private readonly IAsyncRepository<Exercise> _exerciseRepository;
    private readonly IMapper _mapper;

    public GetExercisesListQueryHandler(IAsyncRepository<Exercise> exerciseRepository, IMapper mapper)
    {
        _exerciseRepository = exerciseRepository;
        _mapper = mapper;
    }

    public async Task<GetExercisesListQueryResponse> Handle(GetExercisesListQuery request, CancellationToken cancellationToken)
    {
        var exerciseList = (await _exerciseRepository.ListAllAsync()).OrderBy(e => e.Name);

        return new GetExercisesListQueryResponse
        {
            Success = true,
            ExerciseList = _mapper.Map<List<ExerciseListVm>>(exerciseList)
        };
    }
}
