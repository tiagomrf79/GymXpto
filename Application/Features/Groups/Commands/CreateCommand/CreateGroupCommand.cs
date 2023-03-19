using MediatR;

namespace Application.Features.Groups.Commands.CreateCommand;

public class CreateGroupCommand : IRequest<CreateGroupCommandResponse>
{
    public Guid WorkoutId { get; set; }
    public int Order { get; set; }
    public int RestBetweenSets { get; set; }
}
