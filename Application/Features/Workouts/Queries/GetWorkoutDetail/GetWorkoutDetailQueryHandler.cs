using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Workouts.Queries.GetWorkoutDetail;

public class GetWorkoutDetailQueryHandler : IRequestHandler<GetWorkoutDetailQuery, GetWorkoutDetailQueryResponse>
{
    private readonly IAsyncRepository<Workout> _workoutRepository;
    private readonly IMapper _mapper;

    public GetWorkoutDetailQueryHandler(IAsyncRepository<Workout> workoutRepository, IMapper mapper)
    {
        _workoutRepository = workoutRepository;
        _mapper = mapper;
    }


    public async Task<GetWorkoutDetailQueryResponse> Handle(GetWorkoutDetailQuery request, CancellationToken cancellationToken)
    {
        var queryResponse = new GetWorkoutDetailQueryResponse();
        var entityFound = await _workoutRepository.GetByIdAsync(request.WorkoutId);

        if (entityFound == null)
        {
            queryResponse.Success = false;
            queryResponse.Message = "Workout not found.";
        }

        if (queryResponse.Success)
        {
            queryResponse.Workout = _mapper.Map<WorkoutDetailDto>(entityFound);
        }
        return queryResponse;
    }
}
