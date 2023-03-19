namespace Application.Features.Groups.Queries.GetGroupDetail;

public class GroupDetailDto
{
    public Guid GroupId { get; set; }
    public Guid WorkoutId { get; set; }
    public int Order { get; set; }
    public int RestBetweenSets { get; set; }
}