using MediatR;

namespace Application.Features.Supersets.Commands.DeleteSuperset;

public class DeleteSupersetCommand : IRequest<DeleteSupersetCommandResponse>
{
    public Guid SupersetId { get; set; }
}
