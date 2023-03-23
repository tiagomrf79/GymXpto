using Application.Interfaces.Persistence;
using MediatR;

namespace Application.Features.Supersets.Commands.DeleteSuperset;

public class DeleteSupersetCommandHandler : IRequestHandler<DeleteSupersetCommand, DeleteSupersetCommandResponse>
{
    private readonly ISupersetRepository _supersetRepository;

    public DeleteSupersetCommandHandler(ISupersetRepository supersetRepository)
    {
        _supersetRepository = supersetRepository;
    }

    public async Task<DeleteSupersetCommandResponse> Handle(DeleteSupersetCommand request, CancellationToken cancellationToken)
    {
        var supersetToDelete = await _supersetRepository.GetByIdAsync(request.SupersetId);

        if (supersetToDelete == null)
        {
            return new DeleteSupersetCommandResponse
            {
                Success = false,
                Message = "Superset not found."
            };
        }

        await _supersetRepository.DeleteAsync(supersetToDelete);

        return new DeleteSupersetCommandResponse
        {
            Success = true
        };
    }
}
