using Domain.Common;

namespace Domain.Entities.Schedule;

/// <summary>
/// One 'Workout' represents the sequence of exercises done in one gym session
/// </summary>
public class Workout : AuditableEntity
{
    public Guid WorkoutId { get; set; }
    public string Title { get; set; } = string.Empty;
    public Guid RoutineId { get; set; } //foreign key
    public Routine Routine { get; set; } = null!; //reference navigation property
    public IList<Group> ExerciseSequence { get; private set; } = new List<Group>();
}