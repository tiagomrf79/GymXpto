using Application.Features.Routines.Commands.UpdateRoutine;
using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Routines.Queries.GetRoutineDetail;

public class GetRoutineDetailQueryHandler : IRequestHandler<GetRoutineDetailQuery, GetRoutineDetailQueryResponse>
{
    private readonly IAsyncRepository<Routine> _routineRepository;
    private readonly IMapper _mapper;

    public GetRoutineDetailQueryHandler(IAsyncRepository<Routine> routineRepository, IMapper mapper)
    {
        _routineRepository = routineRepository;
        _mapper = mapper;
    }

    public async Task<GetRoutineDetailQueryResponse> Handle(GetRoutineDetailQuery request, CancellationToken cancellationToken)
    {
        var routineFound = await _routineRepository.GetByIdAsync(request.RoutineId);
        
        if (routineFound == null)
        {
            return new GetRoutineDetailQueryResponse
            {
                Success = false,
                Message = "Routine not found."
            };
        }

        return new GetRoutineDetailQueryResponse
        {
            Success = true,
            Routine = _mapper.Map<RoutineDetailDto>(routineFound)
        };
    }
}
