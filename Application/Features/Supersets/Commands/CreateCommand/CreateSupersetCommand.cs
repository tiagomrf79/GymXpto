using MediatR;

namespace Application.Features.Supersets.Commands.CreateCommand;

public class CreateSupersetCommand : IRequest<CreateSupersetCommandResponse>
{
    public Guid GroupId { get; set; }
    public int Order { get; set; }
}
