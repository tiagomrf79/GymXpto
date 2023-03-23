using Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.Workouts.Queries.GetWorkoutsFromRoutine;

public class GetWorkoutsFromRoutineQueryHandler : IRequestHandler<GetWorkoutsFromRoutineQuery, GetWorkoutsFromRoutineQueryResponse>
{
    private readonly IWorkoutRepository _workoutRepository;
    private readonly IRoutineRepository _routineRepository;
    private readonly IMapper _mapper;

    public GetWorkoutsFromRoutineQueryHandler(IWorkoutRepository workoutRepository, IRoutineRepository routineRepository, IMapper mapper)
    {
        _workoutRepository = workoutRepository;
        _routineRepository = routineRepository;
        _mapper = mapper;
    }


    public async Task<GetWorkoutsFromRoutineQueryResponse> Handle(GetWorkoutsFromRoutineQuery request, CancellationToken cancellationToken)
    {
        var routineFound = await _routineRepository.GetByIdAsync(request.RoutineId);

        if (routineFound == null)
        {
            return new GetWorkoutsFromRoutineQueryResponse
            {
                Success = false,
                Message = "Routine not found."
            };
        }

        var workoutList = await _workoutRepository.GetWorkoutsFromRoutine(request.RoutineId);

        return new GetWorkoutsFromRoutineQueryResponse
        {
            Success = true,
            WorkoutList = _mapper.Map<List<WorkoutListDto>>(workoutList)
        };
    }
}
