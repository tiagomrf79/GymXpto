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
        var routineToUpdate = await _routineRepository.GetByIdAsync(request.RoutineId);

        if (routineToUpdate == null)
        {
            return new UpdateRoutineCommandResponse
            {
                Success = false,
                Message = "Routine not found."
            };
        }

        var validator = new UpdateRoutineCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return new UpdateRoutineCommandResponse
            {
                Success = false,
                ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }

        _mapper.Map(request, routineToUpdate, typeof(UpdateRoutineCommand), typeof(Routine));
        await _routineRepository.UpdateAsync(routineToUpdate);

        return new UpdateRoutineCommandResponse
        {
            Success = true,
            Routine = _mapper.Map<UpdateRoutineDto>(routineToUpdate)
        };
    }
}
