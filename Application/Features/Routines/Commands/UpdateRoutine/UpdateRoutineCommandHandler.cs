using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Routines.Commands.UpdateRoutine;

public class UpdateRoutineCommandHandler : IRequestHandler<UpdateRoutineCommand, UpdateRoutineCommandResponse>
{
    private readonly IAsyncRepository<Routine> _routineRepository;
    private readonly IMapper _mapper;

    public UpdateRoutineCommandHandler(IAsyncRepository<Routine> routineRepository, IMapper mapper)
    {
        _routineRepository = routineRepository;
        _mapper = mapper;
    }


    public async Task<UpdateRoutineCommandResponse> Handle(UpdateRoutineCommand request, CancellationToken cancellationToken)
    {
        var updateRoutineCommandResponse = new UpdateRoutineCommandResponse();
        var routineToUpdate = await _routineRepository.GetByIdAsync(request.RoutineId);

        if (routineToUpdate == null)
        {
            updateRoutineCommandResponse.Success = false;
            updateRoutineCommandResponse.Message = "Routine not found.";
        }
        else
        {
            var validator = new UpdateRoutineCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                updateRoutineCommandResponse.Success = false;
                updateRoutineCommandResponse.ValidationErrors = new List<string>();

                foreach (var error in validationResult.Errors)
                {
                    updateRoutineCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }
        }

        if (updateRoutineCommandResponse.Success)
        {
            _mapper.Map(request, routineToUpdate, typeof(UpdateRoutineCommand), typeof(Routine));
            await _routineRepository.UpdateAsync(routineToUpdate!);
            updateRoutineCommandResponse.Routine = _mapper.Map<UpdateRoutineDto>(routineToUpdate);
        }

        return updateRoutineCommandResponse;
    }
}
