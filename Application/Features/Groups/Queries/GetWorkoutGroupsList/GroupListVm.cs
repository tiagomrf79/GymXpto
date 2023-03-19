namespace Application.Features.Groups.Queries.GetWorkoutGroupsList;

public class GroupListVm
{
    public Guid GroupId { get; set; }
    public Guid WorkoutId { get; set; }
    public int Order { get; set; }
    public int RestBetweenSets { get; set; }
}