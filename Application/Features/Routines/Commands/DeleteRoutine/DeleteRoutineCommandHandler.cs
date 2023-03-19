using Application.Interfaces.Persistence;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Routines.Commands.DeleteRoutine;

public class DeleteRoutineCommandHandler : IRequestHandler<DeleteRoutineCommand, DeleteRoutineCommandResponse>
{
    private readonly IAsyncRepository<Routine> _routineRepository;

    public DeleteRoutineCommandHandler(IAsyncRepository<Routine> routineRepository)
    {
        _routineRepository = routineRepository;
    }

    public async Task<DeleteRoutineCommandResponse> Handle(DeleteRoutineCommand request, CancellationToken cancellationToken)
    {
        var routineToDelete = await _routineRepository.GetByIdAsync(request.RoutineId);

        if (routineToDelete == null)
        {
            return new DeleteRoutineCommandResponse
            {
                Success = false,
                Message = "Routine not found."
            };
        }

        await _routineRepository.DeleteAsync(routineToDelete);

        return new DeleteRoutineCommandResponse
        {
            Success = true
        };
    }
}
