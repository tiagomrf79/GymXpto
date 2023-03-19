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
        var routineFound = await _routineRepository.GetByIdAsync(request.RoutineId);

        if (routineFound == null)
        {
            return new GetRoutineWorkoutsListQueryResponse
            {
                Success = false,
                Message = "Routine not found."
            };
        }

        var workoutsList = (await _workoutRepository.ListAllAsync()).Where(w => w.RoutineId == request.RoutineId);

        return new GetRoutineWorkoutsListQueryResponse
        {
            Success = true,
            WorkoutsList = _mapper.Map<List<WorkoutListVm>>(workoutsList)
        };
    }
}
