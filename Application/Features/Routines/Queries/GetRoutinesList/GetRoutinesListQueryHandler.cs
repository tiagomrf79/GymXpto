using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Routines.Queries.GetRoutinesList;

public class GetRoutinesListQueryHandler : IRequestHandler<GetRoutinesListQuery, List<RoutineListVm>>
{
    private readonly IAsyncRepository<Routine> _routineRepository;
    private readonly IMapper _mapper;

    public GetRoutinesListQueryHandler(IAsyncRepository<Routine> routineRepository, IMapper mapper)
    {
        _routineRepository = routineRepository;
        _mapper = mapper;
    }

    public async Task<List<RoutineListVm>> Handle(GetRoutinesListQuery request, CancellationToken cancellationToken)
    {
        var allRoutines = (await _routineRepository.ListAllAsync()).OrderBy(r => r.Title);
        return _mapper.Map<List<RoutineListVm>>(allRoutines);
    }
}
