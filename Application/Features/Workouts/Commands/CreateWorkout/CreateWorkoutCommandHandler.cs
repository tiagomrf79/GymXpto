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
        var commandResponse = new CreateWorkoutCommandResponse();
        var routineFound = await _routineRepository.GetByIdAsync(request.RoutineId);

        if (routineFound == null)
        {
            commandResponse.Success = false;
            commandResponse.Message = "Routine not found.";
        }
        else
        {
            var validator = new CreateWorkoutCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                commandResponse.Success = false;
                commandResponse.ValidationErrors = new List<string>();

                foreach (var error in validationResult.Errors)
                {
                    commandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }
        }

        if (commandResponse.Success)
        {
            var newEntity = new Workout()
            {
                RoutineId = request.RoutineId,
                Title = request.Title
            };

            newEntity = await _workoutRepository.AddAsync(newEntity);
            commandResponse.Workout = _mapper.Map<CreateWorkoutDto>(newEntity);
        }

        return commandResponse;
    }
}
