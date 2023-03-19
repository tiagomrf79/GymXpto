using MediatR;

namespace Application.Features.Supersets.Commands.DeleteCommand;

public class DeleteSupersetCommand : IRequest<DeleteSupersetCommandResponse>
{
    public Guid SupersetId { get; set; }
}
