using MediatR;

namespace Application.Features.Groups.Commands.UpdateCommand;

public class UpdateGroupCommand : IRequest<UpdateGroupCommandResponse>
{
    public Guid GroupId { get; set; }
    public Guid WorkoutId { get; set; }
    public int Order { get; set; } //position in workout
    public int RestBetweenSets { get; set; }
}
