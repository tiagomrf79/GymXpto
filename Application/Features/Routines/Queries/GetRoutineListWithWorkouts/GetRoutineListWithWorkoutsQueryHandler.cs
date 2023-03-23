using Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.Routines.Queries.GetRoutineListWithWorkouts;

public class GetRoutineListWithWorkoutsQueryHandler : IRequestHandler<GetRoutineListWithWorkoutsQuery, GetRoutineListWithWorkoutsQueryResponse>
{
    private readonly IRoutineRepository _routineRepository;
    private readonly IMapper _mapper;

    public GetRoutineListWithWorkoutsQueryHandler(IRoutineRepository routineRepository, IMapper mapper)
    {
        _routineRepository = routineRepository;
        _mapper = mapper;
    }


    public async Task<GetRoutineListWithWorkoutsQueryResponse> Handle(GetRoutineListWithWorkoutsQuery request, CancellationToken cancellationToken)
    {
        var routineList = await _routineRepository.GetRoutineWithWorkouts();

        return new GetRoutineListWithWorkoutsQueryResponse
        {
            Success = true,
            RoutineWorkoutsList = _mapper.Map<List<RoutineWorkoutsListDto>>(routineList)
        };
    }
}
