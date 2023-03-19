using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.ExerciseSets.Queries.GetSupersetExerciseSetsList;

public class GetSupersetExerciseSetsListQueryHandler : IRequestHandler<GetSupersetExerciseSetsListQuery, GetSupersetExerciseSetsListQueryResponse>
{
    private readonly IAsyncRepository<ExerciseSet> _exerciseSetRepository;
    private readonly IAsyncRepository<Superset> _supersetRepository;
    private readonly IMapper _mapper;

    public GetSupersetExerciseSetsListQueryHandler(IAsyncRepository<ExerciseSet> exerciseSetRepository,
        IAsyncRepository<Superset> supersetRepository, IMapper mapper)
    {
        _exerciseSetRepository = exerciseSetRepository;
        _supersetRepository = supersetRepository;
        _mapper = mapper;
    }

    public async Task<GetSupersetExerciseSetsListQueryResponse> Handle(GetSupersetExerciseSetsListQuery request, CancellationToken cancellationToken)
    {
        var supersetFound = await _supersetRepository.GetByIdAsync(request.SupersetId);

        if (supersetFound == null)
        {
            return new GetSupersetExerciseSetsListQueryResponse
            {
                Success = false,
                Message = "Superset not found."
            };
        }

        var exerciseSetList = (await _exerciseSetRepository.ListAllAsync()).Where(e => e.SupersetId == request.SupersetId);

        return new GetSupersetExerciseSetsListQueryResponse
        {
            Success = true,
            ExerciseSetList = _mapper.Map<List<ExerciseSetListVm>>(exerciseSetList)
        };
    }
}
