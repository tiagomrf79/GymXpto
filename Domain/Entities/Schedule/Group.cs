using Domain.Common;

namespace Domain.Entities.Schedule;

/// <summary>
/// One 'Group' represents executing a number of sets of one or more exercises.
/// For example: executing 3 sets of Dumbbell Squats with 2 minutes of rest between each set.
/// </summary>
public class Group : AuditableEntity
{
    public Guid GroupId { get; set; }
    public Guid WorkoutId { get; set; }
    public int Order { get; set; } //position in workout
    public int RestBetweenSets { get; set; }
    public IList<Superset> Sets { get; private set; } = new List<Superset>();
}