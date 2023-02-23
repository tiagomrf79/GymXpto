using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Routines.Queries.GetRoutinesListWithWorkouts;

public class GetRoutinesListWithWorkoutsQueryHandler : IRequestHandler<GetRoutinesListWithWorkoutsQuery, List<RoutineWorkoutsListVm>>
{
    private readonly IRoutineRepository _routineRepository;
    private readonly IMapper _mapper;

    public GetRoutinesListWithWorkoutsQueryHandler(IRoutineRepository routineRepository, IMapper mapper)
    {
        _routineRepository = routineRepository;
        _mapper = mapper;
    }


    public async Task<List<RoutineWorkoutsListVm>> Handle(GetRoutinesListWithWorkoutsQuery request, CancellationToken cancellationToken)
    {
        var allRotinesWithEvents = await _routineRepository.GetRoutineWithWorkouts();
        return _mapper.Map<List<RoutineWorkoutsListVm>>(allRotinesWithEvents);
    }
}
