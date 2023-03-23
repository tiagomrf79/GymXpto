using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Routines.Queries.GetRoutineList;

public class GetRoutineListQueryHandler : IRequestHandler<GetRoutineListQuery, GetRoutineListQueryResponse>
{
    private readonly IAsyncRepository<Routine> _routineRepository;
    private readonly IMapper _mapper;

    public GetRoutineListQueryHandler(IAsyncRepository<Routine> routineRepository, IMapper mapper)
    {
        _routineRepository = routineRepository;
        _mapper = mapper;
    }

    public async Task<GetRoutineListQueryResponse> Handle(GetRoutineListQuery request, CancellationToken cancellationToken)
    {
        var routinesList = (await _routineRepository.ListAllAsync()).OrderBy(r => r.Title);

        return new GetRoutineListQueryResponse
        {
            Success = true,
            RoutineList = _mapper.Map<List<RoutineListDto>>(routinesList)
        };
    }
}
