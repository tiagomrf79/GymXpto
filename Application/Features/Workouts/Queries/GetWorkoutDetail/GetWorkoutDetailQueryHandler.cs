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
        var workoutFound = await _workoutRepository.GetByIdAsync(request.WorkoutId);

        if (workoutFound == null)
        {
            return new GetWorkoutDetailQueryResponse
            {
                Success = false,
                Message = "Workout not found."
            };
        }

        return new GetWorkoutDetailQueryResponse
        {
            Success = true,
            Workout = _mapper.Map<WorkoutDetailDto>(workoutFound)
        };
    }
}
