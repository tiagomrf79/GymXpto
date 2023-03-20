using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Workouts.Commands.CreateWorkout;

public class CreateWorkoutCommandHandler : IRequestHandler<CreateWorkoutCommand, CreateWorkoutCommandResponse>
{
    private readonly IAsyncRepository<Workout> _workoutRepository;
    private readonly IAsyncRepository<Routine> _routineRepository;
    private readonly IMapper _mapper;

    public CreateWorkoutCommandHandler(IAsyncRepository<Workout> workoutRepository, IAsyncRepository<Routine> routineRepository, IMapper mapper)
    {
        _workoutRepository = workoutRepository;
        _routineRepository = routineRepository;
        _mapper = mapper;
    }

    public async Task<CreateWorkoutCommandResponse> Handle(CreateWorkoutCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateWorkoutCommandValidator(_routineRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return new CreateWorkoutCommandResponse
            {
                Success = false,
                ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }

        var newWorkout = new Workout()
        {
            RoutineId = request.RoutineId,
            Title = request.Title
        };

        newWorkout = await _workoutRepository.AddAsync(newWorkout);

        return new CreateWorkoutCommandResponse
        {
            Success = true,
            Workout = _mapper.Map<CreateWorkoutDto>(newWorkout)
        };
    }
}
