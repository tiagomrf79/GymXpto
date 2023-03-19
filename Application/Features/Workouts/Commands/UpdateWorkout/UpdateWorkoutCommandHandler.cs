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
        var workoutToUpdate = await _workoutRepository.GetByIdAsync(request.WorkoutId);

        if (workoutToUpdate == null)
        {
            return new UpdateWorkoutCommandResponse
            {
                Success = false,
                Message = "Workout not found."
            };
        }

        var routineFound = await _routineRepository.GetByIdAsync(request.RoutineId);

        if (routineFound == null)
        {
            return new UpdateWorkoutCommandResponse
            {
                Success = false,
                Message = "Routine not found."
            };
        }

        var validator = new UpdateWorkoutCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return new UpdateWorkoutCommandResponse
            {
                Success = false,
                ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }

        _mapper.Map(request, workoutToUpdate, typeof(UpdateWorkoutCommand), typeof(Workout));
        await _workoutRepository.UpdateAsync(workoutToUpdate);

        return new UpdateWorkoutCommandResponse
        {
            Success = true,
            Workout = _mapper.Map<UpdateWorkoutDto>(workoutToUpdate)
        };
    }
}
