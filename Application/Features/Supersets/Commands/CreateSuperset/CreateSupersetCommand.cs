using MediatR;

namespace Application.Features.Supersets.Commands.CreateSuperset;

public class CreateSupersetCommand : IRequest<CreateSupersetCommandResponse>
{
    public Guid GroupId { get; set; }
    public int Order { get; set; }
}
