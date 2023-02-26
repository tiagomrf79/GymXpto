using Application.Features.Routines.Queries.GetRoutinesList;
using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Workouts.Queries.GetRoutineWorkoutsList;

public class GetRoutineWorkoutsListQueryHandler : IRequestHandler<GetRoutineWorkoutsListQuery, GetRoutineWorkoutsListQueryResponse>
{
    private readonly IAsyncRepository<Workout> _workoutRepository;
    private readonly IAsyncRepository<Routine> _routineRepository;
    private readonly IMapper _mapper;

    public GetRoutineWorkoutsListQueryHandler(IAsyncRepository<Workout> workoutRepository, IAsyncRepository<Routine> routineRepository, IMapper mapper)
    {
        _workoutRepository = workoutRepository;
        _routineRepository = routineRepository;
        _mapper = mapper;
    }


    public async Task<GetRoutineWorkoutsListQueryResponse> Handle(GetRoutineWorkoutsListQuery request, CancellationToken cancellationToken)
    {
        var queryResponse = new GetRoutineWorkoutsListQueryResponse();
        var routineFound = await _routineRepository.GetByIdAsync(request.RoutineId);

        if (routineFound == null)
        {
            queryResponse.Success = false;
            queryResponse.Message = "Routine not found.";
        }

        if (queryResponse.Success)
        {
            var workoutsList = (await _workoutRepository.ListAllAsync()).Where(w => w.RoutineId == request.RoutineId);
            queryResponse.WorkoutsList = _mapper.Map<List<WorkoutListVm>>(workoutsList);
        }

        return queryResponse;
    }
}
