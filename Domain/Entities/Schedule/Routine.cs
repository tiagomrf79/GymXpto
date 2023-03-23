using Domain.Common;

namespace Domain.Entities.Schedule;

/// <summary>
/// One 'Routine' represents a training plan created for one person, usually with more than one workout to be used on different sessions
/// </summary>
public class Routine : AuditableEntity
{
    public Guid RoutineId { get; set; } //principal key (primary key)

    //TODO: Add UserId
    //public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public IList<Workout> Workouts { get; private set; } = new List<Workout>(); //collection navigation property
}
