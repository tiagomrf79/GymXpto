using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Routines.Queries.GetRoutinesListWithWorkouts;

public class GetRoutinesListWithWorkoutsQueryHandler : IRequestHandler<GetRoutinesListWithWorkoutsQuery, GetRoutinesListWithWorkoutsQueryResponse>
{
    private readonly IRoutineRepository _routineRepository;
    private readonly IMapper _mapper;

    public GetRoutinesListWithWorkoutsQueryHandler(IRoutineRepository routineRepository, IMapper mapper)
    {
        _routineRepository = routineRepository;
        _mapper = mapper;
    }


    public async Task<GetRoutinesListWithWorkoutsQueryResponse> Handle(GetRoutinesListWithWorkoutsQuery request, CancellationToken cancellationToken)
    {
        var queryResponse = new GetRoutinesListWithWorkoutsQueryResponse();

        var routinesList = await _routineRepository.GetRoutineWithWorkouts();
        queryResponse.RoutineWorkoutsList = _mapper.Map<List<RoutineWorkoutsListVm>>(routinesList);

        return queryResponse;
    }
}
