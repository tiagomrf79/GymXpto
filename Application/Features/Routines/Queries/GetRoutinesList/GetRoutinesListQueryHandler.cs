using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Routines.Queries.GetRoutinesList;

public class GetRoutinesListQueryHandler : IRequestHandler<GetRoutinesListQuery, GetRoutinesListQueryResponse>
{
    private readonly IAsyncRepository<Routine> _routineRepository;
    private readonly IMapper _mapper;

    public GetRoutinesListQueryHandler(IAsyncRepository<Routine> routineRepository, IMapper mapper)
    {
        _routineRepository = routineRepository;
        _mapper = mapper;
    }

    public async Task<GetRoutinesListQueryResponse> Handle(GetRoutinesListQuery request, CancellationToken cancellationToken)
    {
        var queryResponse = new GetRoutinesListQueryResponse();

        var routinesList = (await _routineRepository.ListAllAsync()).OrderBy(r => r.Title);
        queryResponse.RoutinesList = _mapper.Map<List<RoutineListVm>>(routinesList);

        return queryResponse;
    }
}
