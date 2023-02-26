using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Workouts.Commands.UpdateWorkout;

public class UpdateWorkoutCommandHandler : IRequestHandler<UpdateWorkoutCommand, UpdateWorkoutCommandResponse>
{
    private readonly IAsyncRepository<Workout> _workoutRepository;
    private readonly IAsyncRepository<Routine> _routineRepository;
    private readonly IMapper _mapper;

    public UpdateWorkoutCommandHandler(IAsyncRepository<Workout> workoutRepository, IAsyncRepository<Routine> routineRepository, IMapper mapper)
    {
        _workoutRepository = workoutRepository;
        _routineRepository = routineRepository;
        _mapper = mapper;
    }


    public async Task<UpdateWorkoutCommandResponse> Handle(UpdateWorkoutCommand request, CancellationToken cancellationToken)
    {
        var commandResponse = new UpdateWorkoutCommandResponse();
        var entityToUpdate = await _workoutRepository.GetByIdAsync(request.WorkoutId);
        var routineFound = await _routineRepository.GetByIdAsync(request.RoutineId);

        if (entityToUpdate == null)
        {
            commandResponse.Success = false;
            commandResponse.Message = "Workout not found.";
        }
        else if (routineFound == null)
        {
            commandResponse.Success = false;
            commandResponse.Message = "Routine not found.";
        }
        else
        {
            var validator = new UpdateWorkoutCommandValidator();
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
            _mapper.Map(request, entityToUpdate, typeof(UpdateWorkoutCommand), typeof(Workout));
            await _workoutRepository.UpdateAsync(entityToUpdate!);
            commandResponse.Workout = _mapper.Map<UpdateWorkoutDto>(entityToUpdate);
        }

        return commandResponse;
    }
}
