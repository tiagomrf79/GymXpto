using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Routines.Commands.DeleteRoutine;

internal class DeleteRoutineCommandHandler : IRequestHandler<DeleteRoutineCommand, DeleteRoutineCommandResponse>
{
    private readonly IAsyncRepository<Routine> _routineRepository;
    private readonly IMapper _mapper;

    public DeleteRoutineCommandHandler(IAsyncRepository<Routine> routineRepository, IMapper mapper)
    {
        _routineRepository = routineRepository;
        _mapper = mapper;
    }

    public async Task<DeleteRoutineCommandResponse> Handle(DeleteRoutineCommand request, CancellationToken cancellationToken)
    {
        var deleteRoutineCommandResponse = new DeleteRoutineCommandResponse();
        var responseToDelete = await _routineRepository.GetByIdAsync(request.RoutineId);

        if (responseToDelete == null)
        {
            deleteRoutineCommandResponse.Success = false;
            deleteRoutineCommandResponse.Message = "Routine not found.";
        }

        if (deleteRoutineCommandResponse.Success)
        {
            await _routineRepository.DeleteAsync(responseToDelete!);
        }

        return deleteRoutineCommandResponse;
    }
}
