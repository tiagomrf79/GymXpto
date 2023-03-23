using Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.ExerciseSets.Queries.GetExerciseSetsFromSuperset;

public class GetExerciseSetsFromSupersetQueryHandler : IRequestHandler<GetExerciseSetsFromSupersetQuery, GetExerciseSetsFromSupersetQueryResponse>
{
    private readonly IExerciseSetRepository _exerciseSetRepository;
    private readonly ISupersetRepository _supersetRepository;
    private readonly IMapper _mapper;

    public GetExerciseSetsFromSupersetQueryHandler(IExerciseSetRepository exerciseSetRepository,
        ISupersetRepository supersetRepository, IMapper mapper)
    {
        _exerciseSetRepository = exerciseSetRepository;
        _supersetRepository = supersetRepository;
        _mapper = mapper;
    }

    public async Task<GetExerciseSetsFromSupersetQueryResponse> Handle(GetExerciseSetsFromSupersetQuery request, CancellationToken cancellationToken)
    {
        var supersetFound = await _supersetRepository.GetByIdAsync(request.SupersetId);

        if (supersetFound == null)
        {
            return new GetExerciseSetsFromSupersetQueryResponse
            {
                Success = false,
                Message = "Superset not found."
            };
        }

        var exerciseSetList = await _exerciseSetRepository.GetExerciseSetsFromSuperset(request.SupersetId);

        return new GetExerciseSetsFromSupersetQueryResponse
        {
            Success = true,
            ExerciseSetList = _mapper.Map<List<ExerciseSetListVm>>(exerciseSetList)
        };
    }
}
